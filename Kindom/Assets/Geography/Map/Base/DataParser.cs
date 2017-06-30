using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Geography.Map
{
	/// <summary>
	/// 元数据
	/// </summary>
	internal struct Metadata
	{
		private string _Key;
		private string _Value;

		public string Key {
			get { 
				return _Key; 
			}
		}

		public string Value {
			get { 
				return _Value; 
			}
		}

		public Metadata (string key, string value)
		{
			_Key = key;
			_Value = value;
		}
	}

	/// <summary>
	/// 数据项
	/// </summary>
	internal class DataPair
	{
		public string Key;
		public StringBuilder Value;

		public DataPair (string symbol)
		{
			Key = symbol;
			Value = new StringBuilder ();
		}
	}

	/// <summary>
	/// 递归项
	/// </summary>
	internal class DataLevel
	{
		public int Level;
		public DataPair DataPair;
		public DataLevel Parent;
		public List<DataLevel> Children;

		public DataLevel ()
		{
			Children = new List<DataLevel> ();
		}
	}

	internal delegate bool ParseLine(string line);

	public class DataParser
	{
		private Metadata[] _Metadatas = new Metadata[] {
			new Metadata ("{", "}"),
		};

		private Stack _Stack;

		private Dictionary<char, ParseLine> _ParseLineMethods;

		public DataParser ()
		{
			_Stack = new Stack ();
			_ParseLineMethods = new Dictionary<char, Geography.Map.ParseLine> ();

			_ParseLineMethods.Add ('=', ParseDictionaryMethod);
			//_ParseLineMethods.Add ('', ParseListMethod);
		}

		/// <summary>
		/// 解析字典符号 
		/// key = value 或者 key = { values }
		/// </summary>
		/// <returns><c>true</c>, if equal line method was parsed, <c>false</c> otherwise.</returns>
		/// <param name="line">Line.</param>
		private bool ParseDictionaryMethod (string line)
		{
			if (string.IsNullOrEmpty (line)) {
				return false;
			}
			int idx = line.IndexOf ('=');
			if (idx < 0) {
				return false;
			}

			string key = line.Substring (0, idx);
			DataPair dataPair = new DataPair (key.Trim ());

			string value = line.Substring (idx + 1, line.Length - idx - 1);
			_Stack.Push (dataPair);
			dataPair.Value.Append (value);

			if (string.IsNullOrEmpty (value)) {
				return false;
			}

			if (!value.Contains (_Metadatas [0].Key) && !value.Contains (_Metadatas [0].Value)) { // 数据块
				return true;
			} else if (value.Contains (_Metadatas [0].Key) && value.Contains (_Metadatas [0].Value)) { // 数据块
				return true;
			}

			return false;
		}

		/// <summary>
		/// 默认解析方法
		/// </summary>
		/// <returns><c>true</c>, if parse method was defaulted, <c>false</c> otherwise.</returns>
		/// <param name="line">Line.</param>
		private bool DefaultParseMethod(string line)
		{
			if (_Stack.Count == 0) {
				_Stack.Push (new DataPair (line));
				return false;
			}

			DataPair dataPair = (DataPair)_Stack.Peek ();
			dataPair.Value.Append (line);

			string value = dataPair.Value.ToString ();
			if (string.IsNullOrEmpty (value)) {
				return false;
			}

			if (value.Contains (_Metadatas [0].Key) && value.Contains (_Metadatas [0].Value)) { // 数据块
				return true;
			}

			return false;
		}

		private bool ParseLine (string line)
		{
			foreach (var item in _ParseLineMethods) {
				if (line.IndexOf (item.Key) > 0) {
					return item.Value (line);
				}
			}

			return DefaultParseMethod (line);
		}

		/// <summary>
		/// 设置数据等级
		/// </summary>
		/// <param name="last">Last.</param>
		/// <param name="current">Current.</param>
		private void SetDataLevel (DataLevel last, DataLevel current)
		{
			if (current == null) {
				return;
			}
			if (last == null) {
				return;
			}

			if (last.Parent == null) {
				last.Parent = new DataLevel ();
				last.Parent.Level = last.Level - 1;
				last.Parent.Children.Add (last);
			}

			if (last.Level < current.Level) {
				last.Children.Add (current);
				current.Parent = last;
			} else if (last.Level == current.Level) {
				last.Parent.Children.Add (current);
				current.Parent = last.Parent;
			} else {
				last.Parent.DataPair = current.DataPair;
			}
		}

		/// <summary>
		/// 转化为数据项
		/// </summary>
		/// <returns>The to data item.</returns>
		/// <param name="current">Current.</param>
		private MapData.DataItem ConvertToDataItem (DataLevel current)
		{
			MapData.DataItem dataItem = new MapData.DataItem (current.DataPair.Key);
			dataItem.Value = current.DataPair.Value.ToString ();
			for (int i = 0; i < current.Children.Count; i++) {
				dataItem.Children.Add (current.Children [i].DataPair.Key, ConvertToDataItem (current.Children [i]));
			}

			return dataItem;
		}

		/// <summary>
		/// 添加数据项
		/// </summary>
		/// <param name="dataItems">Data items.</param>
		/// <param name="current">Current.</param>
		private void AppendDataItem (Dictionary<string, MapData.DataItem> dataItems, DataLevel current)
		{
			MapData.DataItem dataItem = ConvertToDataItem (current);
			dataItems.Add (dataItem.Key, dataItem);
		}

		/// <summary>
		/// 创建数据项
		/// </summary>
		/// <param name="dataItems">Data items.</param>
		/// <param name="lastLevel">Last level.</param>
		private void CreateDataItem (Dictionary<string, MapData.DataItem> dataItems, DataLevel lastLevel)
		{
			DataPair dataPair = null;
			DataLevel dataLevel = null;
			dataPair = (DataPair)_Stack.Pop ();
			dataLevel = new DataLevel ();
			dataLevel.Level = _Stack.Count;
			dataLevel.DataPair = dataPair;
			SetDataLevel (lastLevel, dataLevel);
			lastLevel = dataLevel;
			Debug.Log (dataPair.Key + " : " + dataPair.Value);
			if (_Stack.Count == 0) {
				AppendDataItem (dataItems, lastLevel);
			}
		}

		/// <summary>
		/// 载文件
		/// </summary>
		/// <param name="data">Data.</param>
		/// <param name="dataItems">Data items.</param>
		public bool Load (string data, Dictionary<string, MapData.DataItem> dataItems)
		{
			if (string.IsNullOrEmpty (data)) {
				return false;
			}

			dataItems.Clear ();
			_Stack.Clear ();

			// 去除注释
			data = Regex.Replace (data, "(#.*(\r\n))", "");

			data = Regex.Replace (data, "{", "{\r\n");

			data = Regex.Replace (data, "}", "}\r\n");

			// 多个换行
			data = Regex.Replace (data, "(\r\n)+", "\r\n");

			// 制表符
			data = Regex.Replace (data, "\t", " ");

			//data = Regex.Replace (data, @"([w]+)[\s]+ ", @"\1\r\n");

			DataLevel lastLevel = null;

			StringReader sr = new StringReader (data);
			while (true) {
				string line = sr.ReadLine ();
				if (line == null) {
					break;
				}
				if (line.Trim () == string.Empty) {
					continue;
				}

				if (string.IsNullOrEmpty (line)) {
					continue;
				}

				if (ParseLine (line)) {
					CreateDataItem (dataItems, lastLevel);
				}
			}

			return dataItems.Count != 0;
		}
	}
}