using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geography.Map.Document;
using System.Text.RegularExpressions;
using Document;

namespace Geography.Map
{
	/// <summary>
	/// 世界
	/// </summary>
	public class WorldManager : MonoBehaviour 
	{
		public string DefaultFilePath = "Data/default";

		private string GetUrl(string name) {
			name = Regex.Replace (name, "\"", "");
			name = name.Substring (0, name.LastIndexOf ('.'));
			return name;
		}

		/// <summary>
		/// 将字符串数组转为坐标数组
		/// </summary>
		/// <returns>The to vector2 array.</returns>
		/// <param name="data">Data.</param>
		private Vector2[] ConvertToVector2Array(string[] data) {
			if (data == null || data.Length == 0) {
				return null;
			}
			int count = data.Length / 2 - 1;
			Vector2[] vectorAry = new Vector2[count];
			for (int i = 0; i < count; i++) {
				vectorAry [i] = new Vector2 (float.Parse (data [2 * i]), float.Parse (data [2 * i + 1]));
			}

			return vectorAry;
		}

		private Color ConvertToColor (CSVReader.CSVData data)
		{
			Color color = new Color (
				              float.Parse (data [1]),
				              float.Parse (data [2]),
				              float.Parse (data [3]));
			return color;
		}

		/// <summary>
		/// 获取图片
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="name">Name.</param>
		protected Texture GetImage(string name) {
			return ResourceManger.Instance.Get<Texture> ("Image/" + GetUrl(name));
		}

		/// <summary>
		/// 获取文本数据
		/// </summary>
		/// <returns>The data.</returns>
		/// <param name="name">Name.</param>
		protected string GetData(string name) {
			return ResourceManger.Instance.GetString ("Data/" + GetUrl (name));
		}

		void Start() {
			MapData mapData = new MapData();
			mapData.LoadFromFile (DefaultFilePath);
			Texture texture = GetImage (mapData.GetValue("provinces"));
			WorldMap map = Tool.CreateChild<WorldMap> (this);
			map.Image = texture;
			map.Initialize ();
		}
	}
}
