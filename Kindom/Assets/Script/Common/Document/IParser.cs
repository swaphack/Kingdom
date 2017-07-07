using System;

namespace Document
{
	/// <summary>
	/// 解析器
	/// </summary>
	public interface IParser
	{
		/// <summary>
		/// 处理数据
		/// </summary>
		/// <param name="data">Data.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		IElement Parse(string data, int startIndex, out int endIndex);
	}
}

