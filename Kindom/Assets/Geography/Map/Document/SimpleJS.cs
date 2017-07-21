using System;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Geography.Map.Document
{
	public class SimpleJSNode 
	{
		/// <summary>
		/// 名称
		/// </summary>
		public string Name;
		/// <summary>
		/// 坐标点
		/// </summary>
		public Vector2[] Positions;
	}

	public class SimpleJS
	{
		public SimpleJS ()
		{
		}

		public SimpleJSNode Load(string data) 
		{
			if (string.IsNullOrEmpty (data)) {
				return null;
			}
			data = Regex.Replace (data, "[[\r\n]+", "");

			SimpleJSNode node = new SimpleJSNode ();

			string strVal = "var ";
			data = data.Substring (strVal.Length);
			int index = data.IndexOf ('=');
			node.Name = data.Substring (0, index);
			data = data.Substring (index + 1, data.Length - index - 2);
			data = Regex.Replace (data, "],", ";");
			string[] dataAry = data.Split(';');

			node.Positions = new Vector2[dataAry.Length];
			for (int i = 0; i < dataAry.Length; i++) {
				if (string.IsNullOrEmpty (dataAry [i])) {
					continue;
				}
				string[] strVec2 = dataAry [i].Split (',');
				if (strVec2.Length != 2) {
					Debug.Log ("Error " + dataAry [i]);
					continue;
				}
				float x = 0;
				float y = 0;
				if (!float.TryParse (strVec2 [0], out x)) {
					Debug.Log ("Error Parse " + strVec2 [0]);
					continue;
				}
				if (!float.TryParse (strVec2 [1], out y)) {
					Debug.Log ("Error Parse " + strVec2 [1]);
					continue;
				}
				node.Positions [i] = new Vector2 (x, y);
			}

			return node;
		}
	}
}

