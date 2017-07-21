using UnityEngine;
using System.Collections;
using Geography.Ground;
using Common.Utility;

namespace Geography.Ground.Sample
{

	/// <summary>
	/// 草皮层
	/// </summary>
	public class TurfLayer : Layer
	{
		/// <summary>
		/// 草皮纹理
		/// </summary>
		public Texture2D[] TurfTextures;

		// Use this for initialization
		void Start ()
		{
			TurfTextures = new Texture2D[2];
			TurfTextures [0] = ResourceManger.Instance.Get<Texture2D> ("Textures/Ground/Grass (Hill)");
			TurfTextures [1] = ResourceManger.Instance.Get<Texture2D> ("Textures/Ground/Grass (Lawn)");
			InitGroundPiece ();
		}

		private void InitGroundPiece ()
		{
			if (TurfTextures == null || TurfTextures.Length == 0) {
				return;
			}

			if (!ResourceManger.Instance.LoadGameObject (TilePrefabPath)) {
				Debug.LogError ("null prefabs, url : " + TilePrefabPath);
				return;
			}

			float width = TileCount.Width;
			float height = TileCount.Height;

			int index = 0;
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					Vector3 pos = GetCenterPosition (new Size (i, j));
					index = (i * (int)width + j) % TurfTextures.Length;
					AddTurf (index, pos);
				}
			}
		}

		/// <summary>
		/// 添加草皮
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="pos">Position.</param>
		private void AddTurf (int index, Vector3 pos)
		{
			GameObject go = AddTile<Turf> (pos, true);
			if (go == null) {
				return;
			}

			go.GetComponent<Turf> ().ReplaceTexture (TurfTextures [index]);
		}

		/// <summary>
		/// 替换的纹理资源
		/// </summary>
		public string ReplaceTextureUrl;

		/// <summary>
		/// 点击到点
		/// </summary>
		/// <param name="touchPos">Touch position.</param>
		public override bool OnTouchModel (Vector3 touchPosition)
		{
			Vector3 centerPos = this.ConvertToCenterPosition (touchPosition);
			Turf turf = this.GetTile<Turf> (centerPos);
			if (turf == null || !turf.EnableTouch) {
				return false;
			}
		
			if (!string.IsNullOrEmpty (ReplaceTextureUrl)) {
				turf.ReplaceTexture (ResourceManger.Instance.Get<Texture2D> (ReplaceTextureUrl));
				return true;
			} else {
				return turf.OnTouchModel (touchPosition - turf.OriginPoint);
			}
		}
	}


}