using UnityEngine;
using System.Collections;

namespace Geography.Ground
{
	/// <summary>
	/// 地皮层级
	/// </summary>
	public class Layer : Tile
	{
		/// <summary>
		/// 草皮材质
		/// </summary>
		public string TilePrefabPath = "Prefabs/Tile";

		/// <summary>
		/// 地块离地皮的高度
		/// </summary>
		public const float GROUND_TILE_OFFSET = 0.001f;

		public Layer ()
		{
		}

		/// <summary>
		/// 获取地图块
		/// </summary>
		/// <returns>The tile.</returns>
		/// <param name="centerPos">Center position.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetTile<T> (Vector3 centerPos) where T : Tile
		{
			T[] components = GetComponentsInChildren<T> ();
			if (components == null || components.Length == 0) {
				return null;
			}
			for (int i = 0; i < components.Length; i++) {
				if (components [i].GetComponent<Tile> ().Position == centerPos) {
					return components [i];
				}
			}

			return null;
		}

		/// <summary>
		/// 获取元素索引
		/// </summary>
		/// <returns>The cell index.</returns>
		/// <param name="pos">Position.</param>
		public Size GetCellIndex (Vector3 centerPos)
		{
			Size size = new Size ();
			size.Width = (int)(centerPos.x - OriginPoint.x) / TileSize.Width;
			size.Height = (int)(centerPos.z - OriginPoint.z) / TileSize.Height;
			return size;
		}

		/// <summary>
		/// 获取元素坐标坐标
		/// </summary>
		/// <returns>The center position.</returns>
		/// <param name="centerIndex">Center index.</param>
		public Vector3 GetCenterPosition (Size centerIndex)
		{
			Vector3 pos = new Vector3 ();
			pos.x = centerIndex.Width * TileSize.Width + OriginPoint.x + TileSize.Width * 0.5f;
			pos.y = OriginPoint.y + GROUND_TILE_OFFSET;
			pos.z = centerIndex.Height * TileSize.Height + OriginPoint.z + TileSize.Height * 0.5f;
			return pos;
		}

		/// <summary>
		/// 创建地图块
		/// </summary>
		/// <param name="go">Go.</param>
		/// <param name="pos">Position.</param>
		/// <param name="touchEnable">If set to <c>true</c> touch enable.</param>
		public GameObject AddGameObject<T> (GameObject go, Vector3 pos, bool touchEnable) where T : Tile
		{
			if (go == null || string.IsNullOrEmpty (TilePrefabPath)) {
				return null;
			}
			Vector3 scale = Tool.GetScale (TileSize, Size.one);
			go.name = typeof(T).ToString ();
			go.transform.localScale = scale;
			go.transform.SetParent (this.transform);

			T tile = go.AddComponent<T> ();
			tile.TileSize = TileSize;
			tile.Position = pos;
			tile.EnableTouch = touchEnable;

			Collider collider = go.GetComponent<Collider> ();
			if (collider != null) {
				collider.enabled = false;
			}

			return go;
		}

		/// <summary>
		/// 创建地图块
		/// </summary>
		/// <param name="pos">Position.</param>
		/// <param name="touchEnable">If set to <c>true</c> touch enable.</param>
		public GameObject AddTile<T> (Vector3 pos, bool touchEnable) where T : Tile
		{
			if (string.IsNullOrEmpty (TilePrefabPath)) {
				return null;
			}

			GameObject go = ResourceManger.Instance.CreateGameObject (TilePrefabPath);
			if (go == null) {
				return null;
			}

			return AddGameObject<T> (go, pos, touchEnable);
		}

		/// <summary>
		/// 获取地皮元素中心点坐标
		/// </summary>
		/// <returns>The cell center.</returns>
		/// <param name="pos">Position.</param>
		public Vector3 ConvertToCenterPosition (Vector3 pos)
		{
			pos.x = pos.x - OriginPoint.x;
			pos.z = pos.z - OriginPoint.z;

			Vector3 centerPos = Vector3.zero;

			centerPos.x = (int)(pos.x / TileSize.Width) * TileSize.Width + OriginPoint.x + TileSize.Width * 0.5f;
			centerPos.y = OriginPoint.y + GROUND_TILE_OFFSET;
			centerPos.z = (int)(pos.z / TileSize.Height) * TileSize.Height + OriginPoint.z + TileSize.Height * 0.5f;

			return centerPos;
		}

		/// <summary>
		/// 重新定义大小
		/// </summary>
		/// <param name="tileSize">Tile size.</param>
		/// <param name="tileCount">Tile count.</param>
		public void Resize (Size tileSize, Size tileCount)
		{
			if (this.TileSize == tileSize && this.TileCount == tileCount) {
				return;
			}
			this.TileSize = tileSize;
			this.TileCount = tileCount;
		}


		void Start ()
		{
			InitOriginPoint ();
		}

		/// <summary>
		/// 点击到点
		/// </summary>
		/// <param name="touchPos">Touch position.</param>
		public override bool OnTouchModel (Vector3 touchPosition)
		{
			Vector3 centerPos = this.ConvertToCenterPosition (touchPosition);
			Tile tile = this.GetTile<Tile> (centerPos);
			if (tile == null || !tile.EnableTouch) {
				return false;
			}

			return tile.OnTouchModel (touchPosition - tile.OriginPoint);
		}
	}


}