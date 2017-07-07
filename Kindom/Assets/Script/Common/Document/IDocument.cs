using System;

namespace Document
{
	public interface IDocument
	{
		/// <summary>
		/// 数据
		/// </summary>
		/// <value>The data.</value>
		string Data { get; }
		/// <summary>
		/// 长度
		/// </summary>
		/// <value>The length.</value>
		int Length { get; }
		/// <summary>
		/// 游标所在位置
		/// </summary>
		/// <value>The position.</value>
		int Position { get; set; }
		/// <summary>
		/// 从文件加载
		/// </summary>
		/// <returns><c>true</c>, if from file was loaded, <c>false</c> otherwise.</returns>
		/// <param name="filepath">Filepath.</param>
		bool LoadFromFile(string filepath);
		/// <summary>
		/// 保存到文件
		/// </summary>
		/// <param name="filepath">Filepath.</param>
		void SaveToFile(string filepath);
	}
}

