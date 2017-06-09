using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地皮
/// </summary>
public class Ground : MonoBehaviour {
	
	/// <summary>
	/// 块大小
	/// </summary>
	public Size TileSize = new Size(10, 10);
	/// <summary>
	/// 地皮总数
	/// </summary>
	public Size TileCount = new Size (10, 10);

	/// <summary>
	/// 地块离地皮的高度
	/// </summary>
	public const float GROUND_OFFSET = 0.001f;

	void Start()
	{
		this.transform.localScale = new Vector3 (TileCount.Width, 1, TileCount.Height);
		this.AddLayer<TurfLayer> (GROUND_OFFSET);
		this.AddLayer<BuildingLayer> (2 * GROUND_OFFSET);
	}

	/// <summary>
	/// 添加层
	/// </summary>
	/// <param name="offsetY">Offset y.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public void AddLayer<T>(float offsetY) where T : GroundLayer
	{
		string name = typeof(T).ToString ();
		Vector3 pos = Vector3.zero;
		pos.y = offsetY;

		GameObject go = new GameObject ();
		go.name = name;
		go.transform.localPosition = pos;
		go.transform.SetParent (this.transform);

		T layer = go.AddComponent<T> ();
		layer.TileSize = TileSize;
		layer.TileCount = TileCount;
	}

	/// <summary>
	/// 获取层
	/// </summary>
	/// <returns>The layer.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T GetLayer<T>() where T : GroundLayer
	{
		T layer = this.GetComponentInChildren<T> ();
		if (layer == null) {
			return default(T);
		}

		return layer;
	}
}
