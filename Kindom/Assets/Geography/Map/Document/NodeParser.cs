using System;
using Document;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace Geography.Map.Document
{
	/// <summary>
	/// 节点解析
	/// </summary>
	public class NodeParser : IParser
	{
		public NodeParser() 
		{
			
		}

		/// <summary>
		/// 跳过空字符
		/// </summary>
		/// <returns>The skip empty char.</returns>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		private int SaveSkipEmptyChar(string data, int startIndex)
		{
			do {
				if (startIndex >= data.Length) {
					return startIndex;
				}
				if (data [startIndex] == ' ') {
					startIndex++;
				} else {
					break;
				}
				
			} while (true);

			return startIndex;
		}

		/// <summary>
		/// 解析属性值
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		private string ParseKey(string data, int startIndex, out int endIndex)
		{
			endIndex = 0;

			startIndex = SaveSkipEmptyChar (data, startIndex);
			if (startIndex >= data.Length) {
				return null;
			}

			int len = 0;
			int endIdx = data.IndexOf (' ', startIndex);
			if (endIdx < 0) {
				len = data.Length - startIndex;
				endIdx = data.Length;
			} else {
				len = endIdx - startIndex;
			}

			string key = data.Substring (startIndex, len);

			Match m = Regex.Match (key, "[_0-9a-zA-z]*$");
			if (!m.Success) {
				return null;
			}

			endIndex = endIdx;

			return key;
		}

		/// <summary>
		/// 解析连接符
		/// </summary>
		/// <returns><c>true</c>, if link operator was parsed, <c>false</c> otherwise.</returns>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		private bool ParseLinkOperator(string data, int startIndex, out int endIndex)
		{
			endIndex = 0;

			startIndex = SaveSkipEmptyChar (data, startIndex);
			if (startIndex >= data.Length) {
				return false;
			}

			int len = 0;
			int endIdx = data.IndexOf (' ', startIndex);
			if (endIdx < 0) {
				len = data.Length - startIndex;
				endIdx = data.Length;
			} else {
				len = endIdx - startIndex;
			}

			string key = data.Substring (startIndex, len);
			Match m = Regex.Match (key, "=");
			if (!m.Success) {
				return false;
			}

			endIndex = endIdx;

			return true;
		}

		/// <summary>
		/// 处理数据
		/// </summary>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		public IElement Parse(string data, int startIndex, out int endIndex)
		{
			endIndex = 0;
			int endIdx = 0;

			int off = startIndex;
			bool ret = false;

			// 属性名称
			string key = ParseKey (data, off, out endIdx);
			if (key == null) return null;
			off = endIdx;

			// 连接符
			ret = ParseLinkOperator (data, off, out endIdx);
			if (ret) {
				off = endIdx;
			}

			Node node = new Node ();
			node.Key = key;

			// 属性值
			if (!ParseValue (data, off, node, out endIdx)) {
				return null;
			}

			endIndex = endIdx;

			return node;
		}

		/// <summary>
		/// 解析字符串
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		private String ParseString(string data, int startIndex, IElement node, out int endIndex)
		{
			endIndex = 0;

			int len = 0;
			int endIdx = data.IndexOf (' ', startIndex);
			if (endIdx < 0) {
				len = data.Length - startIndex;
				endIdx = data.Length;
			} else {
				len = endIdx - startIndex;
			}

			string value = data.Substring (startIndex, len);

			Match m1 = Regex.Match (value, @"[+-]?\d+(\.\d+)?$");
			Match m2 = Regex.Match(value, "\"[^\"]*\"");
			Match m3 = Regex.Match(value, "[0-9a-zA-z]+");

			if (!m1.Success && !m2.Success && !m3.Success) {
				return null;
			}

			endIndex = endIdx;

			return value;
		}

		/// <summary>
		/// 解析数组
		/// </summary>
		/// <returns>The array.</returns>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		private string[] ParseArray(string data, int startIndex, IElement node, out int endIndex)
		{
			endIndex = 0;

			if (data [startIndex] != '{') {
				return null;
			}

			int len = 0;

			int endIdx = data.IndexOf ('}', startIndex);
			if (endIdx < 0) {
				return null;
			}

			len = endIdx - startIndex;

			string value = data.Substring (startIndex + 1, len - 1);
			Match m = Regex.Match (value, "[{=}]+");
			if (m.Success) {
				return null;
			}

			value = value.Trim ();

			string[] strAry = value.Split (' ');

			endIndex = endIdx;

			return strAry;
		}

		/// <summary>
		/// 解析子节点
		/// </summary>
		/// <returns><c>true</c>, if children was parsed, <c>false</c> otherwise.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="value">Value.</param>
		private bool ParseChildren(IElement parent, string value)
		{
			int p1 = 0;
			int endIdx = 0;
			while (p1 < value.Length) {
				IElement e = MapFilter.Instance.Match (value, p1, out endIdx);
				if (e != null) {
					parent.AddChild (e);		
					p1 = endIdx;
				} else {
					return false;
				}
				p1++;
			}

			return true;
		}

		/// <summary>
		/// 解析表
		/// </summary>
		/// <returns><c>true</c>, if table was parsed, <c>false</c> otherwise.</returns>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		private bool ParseTable(string data, int startIndex, IElement node, out int endIndex)
		{
			endIndex = 0;

			int endIdx = data.IndexOf ('}', startIndex);
			if (endIdx < 0) {
				return false;
			}

			int pos = startIndex + 1;

			StringBuilder sb = new StringBuilder ();
			while (pos < data.Length) {
				if (data [pos] == '{') {
					string value = sb.ToString ();
					int idx = value.LastIndexOf(' ', value.LastIndexOf('=') - 2);
					idx = idx < 0 ? 0 : idx;
					sb = new StringBuilder(value.Substring(0, idx));
					IElement child = MapFilter.Instance.Match (data, pos - value.Length + idx, out endIdx);
					if (child != null) {
						node.AddChild (child);
						pos = endIdx;
					} else {
						return false;
					}
				} else if (data [pos] == '}') {
					break;
				} else {
					sb.Append (data [pos]);
				}
				pos++;
			}

			if (sb.Length != 0) {
				ParseChildren (node, sb.ToString ());
			}

			endIndex = pos;

			return true;
		}

		/// <summary>
		/// 解析值
		/// </summary>
		/// <returns><c>true</c>, if value was parsed, <c>false</c> otherwise.</returns>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		private bool ParseValue(string data, int startIndex, Node node, out int endIndex) 
		{
			endIndex = 0;

			startIndex = SaveSkipEmptyChar (data, startIndex);
			if (startIndex >= data.Length) {
				return false;
			}

			string strVal = ParseString (data, startIndex, node, out endIndex);
			if (strVal != null) {
				node.SetValue (strVal);
				return true;
			}

			string[] strAry = ParseArray (data, startIndex, node, out endIndex);
			if (strAry != null) {
				node.SetValue (strAry);
				return true;
			}

			return ParseTable (data, startIndex, node, out endIndex);
		}
	}
}

