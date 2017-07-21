using System;
using System.Collections.Generic;

namespace Common.Document
{
	/// <summary>
	/// 文档过滤器
	/// </summary>
	public class DocFilter
	{
		/// <summary>
		/// 解析器列表
		/// </summary>
		private List<IParser> _Parsers;

		public DocFilter() 
		{
			_Parsers = new List<IParser> ();
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
}

