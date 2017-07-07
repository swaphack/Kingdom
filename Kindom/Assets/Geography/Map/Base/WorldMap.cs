using UnityEngine;
using System.Collections;

namespace Geography.Map
{
	/// <summary>
	/// 世界地图
	/// </summary>
	public class WorldMap : Background
	{
		/// <summary>
		/// 图片
		/// </summary>
		public Texture Image;

		protected string matUrl = "Materials/MapBackgroundMat";

		// Use this for initialization
		public override void Initialize ()
		{
			base.Initialize ();
			if (Image == null) {
				return;
			}

			Material mat = ResourceManger.Instance.Get<Material> (matUrl);
			meshRenderer.material = mat;

			Vector3 size = new Vector3 (Image.width, 1, Image.height);
			Mesh2D.CreateTextureMesh (meshFilter, size);
			Mesh2D.CreateTextureMaterial (meshRenderer, Image);

			meshCollider.sharedMesh = meshFilter.mesh;

			TouchListener.Instance.AddDispatch (this.gameObject, OnTouchMe);
		}

		void OnDestory() {
			TouchListener.Instance.RemoveDispatch (this.gameObject);
		}

		/// <summary>
		/// 点击
		/// </summary>
		/// <param name="hitInfo">Hit info.</param>
		private void OnTouchMe(Vector3 hitInfo ) {
			if (Image == null) {
				return;
			}

			Debug.Log (hitInfo);

			float x = hitInfo.x + Image.width * 0.5f * this.transform.localScale.x;
			float z = hitInfo.z + Image.height * 0.5f * this.transform.localScale.z;

			x /= this.transform.localScale.x;
			z /= this.transform.localScale.z;

			Debug.Log (x + "," + z);

			Texture2D texture2D = (Texture2D)Image;
			Color color = texture2D.GetPixel ((int)x, (int)z);

			Debug.Log (color);
		}
	}
}