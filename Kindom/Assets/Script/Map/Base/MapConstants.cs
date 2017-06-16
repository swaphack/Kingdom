﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 常量类
/// </summary>
public class MapConstants
{
	/// <summary>
	/// Panel默认扩展系数10
	/// </summary>
	public const float ExpandRatio = 10;

	/// <summary>
	/// 获取缩放比例
	/// </summary>
	/// <returns>The scale.</returns>
	/// <param name="tileSize">Tile size.</param>
	/// <param name="tileCount">Tile count.</param>
	public static Vector3 GetScale(Size tileSize, Size tileCount)
	{
		return new Vector3 (tileSize.Width * tileCount.Height / ExpandRatio, 1, tileSize.Height * tileCount.Height / ExpandRatio);
	}

	/// <summary>
	/// 获取建筑缩放比例
	/// </summary>
	/// <returns>The building scale.</returns>
	/// <param name="tileSize">Tile size.</param>
	/// <param name="tileCount">Tile count.</param>
	public static Vector3 GetBuildingScale(Size tileSize, Size tileCount)
	{
		float x = tileSize.Width * tileCount.Height / ExpandRatio;
		float z = tileSize.Height * tileCount.Height / ExpandRatio;

		float y = Mathf.Min (x, z);

		return new Vector3 (y, y, y);
	}

	/// <summary>
	/// 获取UI位置
	/// </summary>
	/// <returns>The user interface position y.</returns>
	/// <param name="tileSize">Tile size.</param>
	/// <param name="tileCount">Tile count.</param>
	public static float GetUIPositionY(Size tileSize, Size tileCount)
	{
		float x = tileSize.Width * tileCount.Height / ExpandRatio;
		float z = tileSize.Height * tileCount.Height / ExpandRatio;

		float y = Mathf.Min (x, z);

		return y * ExpandRatio;
	}

	/// <summary>
	/// 获取实际面积
	/// </summary>
	/// <returns>The size.</returns>
	/// <param name="tileSize">Tile size.</param>
	public static Vector2 GetSize(Size tileSize) 
	{
		return new Vector2 (tileSize.Width / ExpandRatio, tileSize.Height / ExpandRatio);		
	}
}
