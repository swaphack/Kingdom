using System;
using Document;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Geography.Map.Document
{
	/// <summary>
	/// 地图文档
	/// </summary>
	public class MapFile : IStructure
	{
		private IElement _Root;

		public MapFile() 
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
			return true;
		}

		/// <summary>
		/// 解析
		/// </summary>
		/// <param name="data">Data.</param>
		public IElement Parse(string data)
		{
			if (string.IsNullOrEmpty (data)) {
				return null;
			}

			int endIdx = 0;
			IElement root = DocFilter.Instance.Match (data, 0, out endIdx);
			if (data.Length != endIdx + 1) {
				Debug.Log ("Not Read End Of Data");
			}
			return root;
		}
	}
}

