using System;
using Common.Document;

namespace Geography.Map.Document.JavaScript
{
	public class JSFile : IStructure
	{
		private IElement _Root;

		public JSFile ()
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
		/// 是否是符合文档结构的数据
		/// </summary>
		/// <param name="data">Data.</param>
		public bool Validate(string data) {
			return true;
		}
		/// <summary>
		/// 格式化数据
		/// </summary>
		/// <param name="data">Data.</param>
		public string Format(string data) {
			return data;
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
		/// 解析数据
		/// </summary>
		public IElement Parse(string data)
		{
			if (string.IsNullOrEmpty (data)) {
				return null;
			}

			JSNode root = new JSNode ();


			return root;
		}
	}
}

