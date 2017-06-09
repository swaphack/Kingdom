using UnityEngine;
using System.Collections;

/// <summary>
/// 建筑层
/// </summary>
public class BuildingLayer : GroundLayer
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


	private void InitGroundPiece() {
		Vector3 pos = this.GetCenterPosition (new Size(5, 5));
		this.AddBuilding (pos, BuildingPrefabUrl);
	}
	
	/// <summary>
	/// Adds the building.
	/// </summary>
	/// <param name="centerPos">Center position.</param>
	/// <param name="url">URL.</param>
	public void AddBuilding(Vector3 centerPos, string url)
	{
		GameObject go = AddTile<Building> (centerPos, false);
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
	public void RemoveBuilding(Building building) {
		if (building == null) {
			return;
		}

		GameObject.Destroy (building.gameObject);
	}
}

