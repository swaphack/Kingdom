using System;

namespace Document
{
	/// <summary>
	/// 结构文档
	/// </summary>
	public interface IStructure
	{
		/// <summary>
		/// 根节点
		/// </summary>
		/// <value>The root.</value>
		IElement Root { get; }
		/// <summary>
		/// 是否是符合文档结构的数据
		/// </summary>
		/// <param name="data">Data.</param>
		bool Validate(string data);
		/// <summary>
		/// 格式化数据
		/// </summary>
		/// <param name="data">Data.</param>
		string Format(string data);
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="data">Data.</param>
		bool Load(string data);
		/// <summary>
		/// 解析数据
		/// </summary>
		IElement Parse(string data);
	}
}

