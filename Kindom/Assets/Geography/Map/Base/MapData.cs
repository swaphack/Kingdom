using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Geography.Map.Document;

namespace Geography.Map
{
	/// <summary>
	/// 数据读取
	/// </summary>
	public class MapData
	{
		public MapData()
		{
		}

		/// <summary>
		/// 从文件加载数据
		/// </summary>
		/// <returns><c>true</c>, if from file was loaded, <c>false</c> otherwise.</returns>
		/// <param name="filepath">Filepath.</param>
		public bool LoadFromFile(string filepath)
		{
			if (string.IsNullOrEmpty (filepath)) {
				return false;
			}

			string data = ResourceManger.Instance.GetString (filepath);
			if (string.IsNullOrEmpty (data)) {
				return false;
			}

			return Load (data);
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

			MapDocument doc = new MapDocument ();
			bool result = doc.Load (data);
			if (!result) {
				return false;
			}

			return true;
		}
	}

}