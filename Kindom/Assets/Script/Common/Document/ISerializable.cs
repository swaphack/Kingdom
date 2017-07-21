using System;

namespace Common.Document
{
	/// <summary>
	/// 可序列化
	/// </summary>
	public interface ISerializable
	{
		/// <summary>
		/// 序列化
		/// </summary>
		void Serialize();
		/// <summary>
		/// 发序列化
		/// </summary>
		void Deserialize();
	}
}