using UnityEngine;
using System.Collections;
using Common.Utility;

namespace Geography.Ground
{
	/// <summary>
	/// 地图元素
	/// </summary>
	public class Tile : ModelBehaviour
	{
		/// <summary>
		/// 原点坐标
		/// </summary>
		private Vector3 _OriginPoint;
		/// <summary>
		/// 块大小
		/// </summary>
		private Size _TileSize;
		/// <summary>
		/// 地皮总数
		/// </summary>
		private Size _TileCount;

		/// <summary>
		/// 是否能点击
		/// </summary>
		/// <value><c>true</c> if touch enable; otherwise, <c>false</c>.</value>
		public override bool EnableTouch {
			get { 
				return _EnableTouch;
			} 
			set { 
				_EnableTouch = value;
			}
		}

		/// <summary>
		/// 块大小
		/// </summary>
		public Size TileSize {
			get { 
				return _TileSize;
			}
			set { 
				_TileSize = value;
				InitOriginPoint ();
			}
		}

		/// <summary>
		/// 地皮总数
		/// </summary>
		public Size TileCount {
			get {
				return _TileCount;
			}
			set { 
				_TileCount = value;
				InitOriginPoint ();
			}
		}

		/// <summary>
		/// 原点坐标
		/// </summary>
		public Vector3 OriginPoint {
			get { 
				return _OriginPoint;
			}
		}

		/// <summary>
		/// 当前坐标
		/// </summary>
		public Vector3 Position {
			get { 
				return this.transform.position;
			}
			set { 
				this.transform.position = value;
				InitOriginPoint ();
			}
		}

		/// <summary>
		/// 是否包含某点
		/// </summary>
		/// <returns><c>true</c>, if point was contained, <c>false</c> otherwise.</returns>
		/// <param name="pos">Position.</param>
		public bool ContainPoint (Vector3 pos)
		{
			if (Position == pos) {
				return true;
			}
			Vector3 originPos = OriginPoint;
			Rect rect = new Rect (originPos.x, originPos.z, TileSize.Width, TileSize.Height);
			return rect.Contains (pos);
		}

		/// <summary>
		/// 初始化原点坐标
		/// </summary>
		protected void InitOriginPoint ()
		{
			Vector3 pos;
			pos.x = this.transform.position.x + (-0.5f * TileSize.Width * TileCount.Width);
			pos.y = this.transform.position.y;
			pos.z = this.transform.position.z + (-0.5f * TileSize.Height * TileCount.Width);
			_OriginPoint = pos;
		}

		public Tile ()
		{
			_TileSize = new Size (1, 1);
			_TileCount = new Size (1, 1);
		}

		private void OnDestroy ()
		{
			EnableTouch = false;
		}

		public override bool OnTouchModel (Vector3 touchPosition)
		{
			return false;
		}
	}


}