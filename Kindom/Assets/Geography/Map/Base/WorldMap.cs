using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geography.Map
{
	/// <summary>
	/// 世界地图
	/// </summary>
	public class WorldMap : MonoBehaviour 
	{
		public string DefaultFilePath = "Data/default";

		void Start() {
			MapData mapData = new MapData();
			mapData.LoadFromFile (DefaultFilePath);
		}
	}
}
