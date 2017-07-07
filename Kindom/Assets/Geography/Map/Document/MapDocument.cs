using System;
using Document;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Geography.Map.Document
{
	public class MapFilter
	{
		private List<IParser> _Parsers;

		private static MapFilter s_Instance;
		public static MapFilter Instance {
			get { 
				if (s_Instance == null) {
					s_Instance = new MapFilter ();
				}

				return s_Instance;
			}
		}


		private MapFilter() 
		{
			_Parsers = new List<IParser> ();

			this.AddParser (new NodeParser ());
		}

		/// <summary>
		/// 配置
		/// </summary>
		/// <param name="data">Data.</param>
		/// <param name="offset">Offset.</param>
		/// <param name="endIndex">End index.</param>
		public IElement Match(string data, int offset, out int endIndex)
		{
			endIndex = 0;

			int off = offset;
			for (int i = 0; i < _Parsers.Count; i++) { 
				IElement e = _Parsers [i].Parse (data, off, out endIndex);
				if (e != null) {
					Debug.Log (e);
					return e;
				}
			}	
			return null;
		}

		/// <summary>
		/// 添加解析器
		/// </summary>
		/// <param name="parser">Parser.</param>
		public void AddParser(IParser parser)
		{
			if (parser == null) {
				return;
			}

			if (!_Parsers.Contains (parser)) {
				_Parsers.Add (parser);
			}
		}
		/// <summary>
		/// 移除解析器
		/// </summary>
		/// <param name="parser">Parser.</param>
		public void RemoveParser(IParser parser)
		{
			if (parser == null) {
				return;
			}

			if (_Parsers.Contains (parser)) {
				_Parsers.Remove (parser);
			}
		}
		/// <summary>
		/// 移除所有解析器
		/// </summary>
		public void RemoveAllParsers()
		{
			_Parsers.Clear ();
		}
	}
	/// <summary>
	/// 地图文档
	/// </summary>
	public class MapDocument : IDocument, IStructure
	{
		private string _Data;
		private int _Position;
		private IElement _Root;

		/// <summary>
		/// 数据
		/// </summary>
		/// <value>The data.</value>
		public string Data { 
			get { 
				return _Data;
			} 
		}
		/// <summary>
		/// 长度
		/// </summary>
		/// <value>The length.</value>
		public int Length { 
			get { 
				return _Data == null ? 0 : _Data.Length;
			}
		}
		/// <summary>
		/// 游标所在位置
		/// </summary>
		/// <value>The position.</value>
		public int Position { 
			get {
				return _Position;		
			} 
			set { 
				_Position = value;
			}
		}

		public MapDocument() 
		{
		}

		/// <summary>
		/// 从文件加载
		/// </summary>
		/// <returns><c>true</c>, if from file was loaded, <c>false</c> otherwise.</returns>
		/// <param name="filepath">Filepath.</param>
		public bool LoadFromFile(string filepath)
		{
			string data = ResourceManger.Instance.GetString (filepath);
			if (string.IsNullOrEmpty (data)) {
				return false;
			}

			return Load (data);
		}
		/// <summary>
		/// 保存到文件
		/// </summary>
		/// <param name="filepath">Filepath.</param>
		public void SaveToFile(string filepath)
		{
			
		}


		/// <summary>
		/// 根节点
		/// </summary>
		/// <value>The root.</value>
		public IElement Root { 
			get {
				return _Root;	
			} 
		}

		/// <summary>
		/// 格式化数据
		/// </summary>
		/// <param name="data">Data.</param>
		public string Format(string data)
		{
			// 去除注释
			data = Regex.Replace (data, "(#.*(\r\n))", "");

			// 补足等号两边空格
			data = Regex.Replace (data, "( )*=( )*", " = ");

			// 特殊符号
			data = Regex.Replace (data, "[ \r\n\t]+", " ");

			// 补齐大括号左边的等号
			data = Regex.Replace (data, "( =)? {", " = {");

			data = data.Trim ();

			data = "root = { " + data + " }";

			return data;
		}

		/// <summary>
		/// 是否是符合文档结构的数据
		/// </summary>
		/// <param name="data">Data.</param>
		public bool Validate(string data)
		{
			if (string.IsNullOrEmpty (data)) {
				return false;
			}

			return true;
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="data">Data.</param>
		public bool Load(string data)
		{
			if (string.IsNullOrEmpty (data)) {
				return false;
			}

			if (!Validate (data)) {
				return false;
			}

			data = Format (data);

			_Root = Parse (data);
			if (_Root == null) {
				return false;
			}
			_Data = data;
			return true;
		}

		public IElement Parse(string data)
		{
			if (string.IsNullOrEmpty (data)) {
				return null;
			}

			int endIdx = 0;
			IElement root = MapFilter.Instance.Match (data, 0, out endIdx);
			if (data.Length != endIdx + 1) {
				Debug.Log ("Not Read End Of Data");
			}
			return root;
		}
	}
}

