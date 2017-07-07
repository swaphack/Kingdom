using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geography.Ground
{
	/// <summary>
	/// 地皮
	/// </summary>
	public class GroundBase : MonoBehaviour, IInitialization
	{
		/// <summary>
		/// 地皮块大小
		/// </summary>
		public Size TileSize = new Size (100, 100);
		/// <summary>
		/// 地皮块总数
		/// </summary>
		public Size TileCount = new Size (10, 10);

		/// <summary>
		/// 地块离地皮的高度
		/// </summary>
		public const float GROUND_OFFSET = 0.001f;

		void Start ()
		{
			Initialize ();

		}

		public virtual void Initialize() {
			this.transform.localScale = Tool.GetScale (TileSize, TileCount);
		}

		/// <summary>
		/// 添加层
		/// </summary>
		/// <param name="offsetY">Offset y.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void AddLayer<T> (float offsetY) where T : Layer
		{
			string name = typeof(T).ToString ();
			Vector3 pos = Vector3.zero;
			pos.y = offsetY;

			GameObject go = new GameObject ();
			go.name = name;
			go.transform.localPosition = pos;
			go.transform.localScale = Vector3.one;
			go.transform.SetParent (this.transform);

			T layer = go.AddComponent<T> ();
			layer.TileSize = TileSize;
			layer.TileCount = TileCount;
		}

		/// <summary>
		/// 获取层
		/// </summary>
		/// <returns>The layer.</returns>
		/// <param name="index">Index.</param>
		public Layer GetLayer (int index)
		{
			if (index < 0 || index >= this.transform.childCount) {
				return null;
			}

			return this.transform.GetChild (index).GetComponent<Layer> ();
		}

		/// <summary>
		/// 层数
		/// </summary>
		/// <value>The layer count.</value>
		public int LayerCount {
			get {
				return this.transform.childCount;	
			}
		}

		/// <summary>
		/// 获取层
		/// </summary>
		/// <returns>The layer.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetLayer<T> () where T : Layer
		{
			T layer = this.GetComponentInChildren<T> ();
			if (layer == null) {
				return default(T);
			}

			return layer;
		}

		/// <summary>
		/// 添加点击监听
		/// </summary>
		public void AddClickListener ()
		{
			TouchListener.Instance.AddDispatch (this.gameObject, this.OnTouchGround);
		}

		/// <summary>
		/// 移除点击监听
		/// </summary>
		public void RemoveClickListener ()
		{
			TouchListener.Instance.RemoveDispatch (this.gameObject);
		}


		/// <summary>
		/// 点击
		/// </summary>
		/// <param name="hitPos">Hit position.</param>
		private void OnTouchGround (Vector3 hitPos)
		{
			Layer[] layers = this.GetComponentsInChildren<Layer> ();
			if (layers == null || layers.Length == 0) {
				return;
			}

			for (int i = layers.Length - 1; i >= 0; i--) {
				ITouchModel layer = layers [i];
				if (layer != null && layer.EnableTouch) {
					layer.OnTouchModel (hitPos);
					return;
				}
			}
		}

		/// <summary>
		/// 设置当前可点击的层，其他层不可点击
		/// </summary>
		/// <param name="index">Index.</param>
		public void SetEnableTouchLayer (int index)
		{
			Layer layer = null;
			for (int i = 0; i < LayerCount; i++) {
				layer = GetLayer (i);
				if (layer != null) {
					if (index == i) {
						layer.EnableTouch = true;
					} else {
						layer.EnableTouch = false;
					}

				}
			}
		}
	}
}