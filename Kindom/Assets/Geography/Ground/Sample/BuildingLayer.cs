using UnityEngine;
using System.Collections;
using Geography.Ground;
using Common.Utility;

namespace Geography.Ground.Sample
{

	/// <summary>
	/// 建筑层
	/// </summary>
	public class BuildingLayer : Layer
	{
		/// <summary>
		/// 建筑预制体
		/// </summary>
		public string BuildingPrefabUrl = "Prefabs/Medieval_Building_19";

		// Use this for initialization
		void Start ()
		{
			InitGroundPiece ();
		}


		private void InitGroundPiece ()
		{
			this.AddBuilding (this.GetCenterPosition (new Size (1, 1)), BuildingPrefabUrl);
			this.AddBuilding (this.GetCenterPosition (new Size (2, 3)), BuildingPrefabUrl);
			this.AddBuilding (this.GetCenterPosition (new Size (4, 5)), BuildingPrefabUrl);
			//this.AddBuilding (this.GetCenterPosition (new Size(5, 5)), BuildingPrefabUrl);
			this.AddBuilding (this.GetCenterPosition (new Size (6, 2)), BuildingPrefabUrl);
			this.AddBuilding (this.GetCenterPosition (new Size (7, 5)), BuildingPrefabUrl);
			this.AddBuilding (this.GetCenterPosition (new Size (1, 5)), BuildingPrefabUrl);
		}

		/// <summary>
		/// Adds the building.
		/// </summary>
		/// <param name="centerPos">Center position.</param>
		/// <param name="url">URL.</param>
		public void AddBuilding (Vector3 centerPos, string url)
		{
			GameObject go = AddTile<Building> (centerPos, true);
			if (go == null) {
				return;
			}

			Building building = go.GetComponent<Building> ();
			building.BuildingPrefab = url;
		}

		/// <summary>
		/// 移除建筑
		/// </summary>
		/// <param name="building">Building.</param>
		public void RemoveBuilding (Building building)
		{
			if (building == null) {
				return;
			}

			GameObject.Destroy (building.gameObject);
		}
	}


}