using UnityEngine;
using System.Collections;

/// <summary>
/// 草皮层
/// </summary>
public class TurfLayer : GroundLayer
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
	
	private void InitGroundPiece() {
		if (TurfTextures == null || TurfTextures.Length == 0) {
			return;
		}

		if (!ResourceManger.Instance.LoadGameObject(TilePrefabPath)) {
			Debug.LogError ("null prefabs, url : " + TilePrefabPath);
			return;
		}

		float width = TileCount.Width;
		float height = TileCount.Height;

		Vector3 pos = Vector3.one;
		pos.y = OriginPoint.y + GROUND_TILE_OFFSET;

		int index = 0;

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				pos.x = i * TileSize.Width + OriginPoint.x + TileSize.Width * 0.5f;
				pos.z = j * TileSize.Height + OriginPoint.z + TileSize.Width * 0.5f;
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
	private void AddTurf(int index, Vector3 pos)
	{
		GameObject go = AddTile<Turf> (pos, false);
		if (go == null) {
			return;
		}

		go.GetComponent<Turf> ().ReplaceMatTexture (TurfTextures [index]);
	}
}

