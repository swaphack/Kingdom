using UnityEngine;
using System.Collections;
using Common.Utility;

namespace Geography.Ground
{
	/// <summary>
	/// 常量类
	/// </summary>
	public class Tool
	{
		/// <summary>
		/// 获取缩放比例
		/// </summary>
		/// <returns>The scale.</returns>
		/// <param name="tileSize">Tile size.</param>
		/// <param name="tileCount">Tile count.</param>
		public static Vector3 GetScale (Size tileSize, Size tileCount)
		{
			return new Vector3 (tileSize.Width * tileCount.Height / Constants.ExpandRatio, 1, tileSize.Height * tileCount.Height / Constants.ExpandRatio);
		}

		/// <summary>
		/// 获取建筑缩放比例
		/// </summary>
		/// <returns>The building scale.</returns>
		/// <param name="tileSize">Tile size.</param>
		/// <param name="tileCount">Tile count.</param>
		public static Vector3 GetBuildingScale (Size tileSize, Size tileCount)
		{
			float x = tileSize.Width * tileCount.Height / Constants.ExpandRatio;
			float z = tileSize.Height * tileCount.Height / Constants.ExpandRatio;

			float y = Mathf.Min (x, z);

			return new Vector3 (y, y, y);
		}

		/// <summary>
		/// 获取UI位置
		/// </summary>
		/// <returns>The user interface position y.</returns>
		/// <param name="tileSize">Tile size.</param>
		/// <param name="tileCount">Tile count.</param>
		public static float GetUIPositionY (Size tileSize, Size tileCount)
		{
			float x = tileSize.Width * tileCount.Height / Constants.ExpandRatio;
			float z = tileSize.Height * tileCount.Height / Constants.ExpandRatio;

			float y = Mathf.Min (x, z);

			return y * Constants.ExpandRatio;
		}

		/// <summary>
		/// 获取实际面积
		/// </summary>
		/// <returns>The size.</returns>
		/// <param name="tileSize">Tile size.</param>
		public static Vector2 GetSize (Size tileSize)
		{
			return new Vector2 (tileSize.Width / Constants.ExpandRatio, tileSize.Height / Constants.ExpandRatio);		
		}
	}

}