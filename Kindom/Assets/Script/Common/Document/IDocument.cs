using System;

namespace Common.Document
{
	public interface IDocument
	{
		/// <summary>
		/// 根节点
		/// </summary>
		/// <value>The root.</value>
		IElement Root { get; }
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

