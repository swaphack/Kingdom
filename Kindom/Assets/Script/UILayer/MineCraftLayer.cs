using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCraftLayer : UILayer 
{
	/// <summary>
	/// 随机种子
	/// </summary>
	public int Seed = 4541;
	/// <summary>
	/// 宽读
	/// </summary>
	public int Width = 20;
	/// <summary>
	/// 长度
	/// </summary>
	public int Length = 20;
	/// <summary>
	/// 持久度
	/// </summary>
	[Range(0,32)]
	public float Persistence = 3;
	/// <summary>
	/// 倍数
	/// </summary>
	[Range(1,10)]
	public int Octaves = 5;
	/// <summary>
	/// 方块纹理
	/// </summary>
	public string[] CubeTextures = new string[] {
		"Textures/blocks/dirt_podzol_top",
		"Textures/blocks/leaves_acacia",
		"Textures/blocks/stone_andesite",
	};
	/// <summary>
	/// 父节点
	/// </summary>
	private GameObject _Parent = null;
	// Use this for initialization
	void Start () {
		_Parent = new GameObject ();
		CreateMap1 ();
	}

	void CreateMap1()
	{
		Vector3 pos = Vector3.zero;
		float h = NoiseHelper.PerlinNoise2D (0, 0, Persistence, Octaves);
		float minH = h;
		float maxH = h;

		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Length; j++) {
				h = NoiseHelper.PerlinNoise2D (i, j, Persistence, Octaves);
				//h = Mathf.PerlinNoise(i, j);
				pos.x = i;
				pos.y = h;
				pos.z = j;
				CubeBuilder.CreateCube (_Parent.transform, pos);	
				maxH = Mathf.Max (h, maxH);
				minH = Mathf.Min (h, minH);
			}
		}

		float avgH = (maxH + minH) * 0.5f;
		minH = (int)(minH - avgH);
		int height = 0;
		int childrenCount = _Parent.transform.childCount;
		for (int i = 0; i < childrenCount; i++) {
			Transform transform = _Parent.transform.GetChild (i);
			pos = transform.position;
			pos.y -= avgH;
			height = (int)pos.y;
			pos.y = height;
			transform.position = pos;
			SetTexture (transform.GetComponent<Cube> ());
			height -= (int)minH;
			if (pos.y > minH) {
				for (int j = 0; j < height; j++) {
					pos.y = j + minH;
					Cube newCube = CubeBuilder.CreateCube (_Parent.transform, pos);
					SetTexture (newCube);
				}
			}
		}
	}

	void CreateMap2()
	{
		Vector3 pos = Vector3.zero;
		float h = 0;
		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Length; j++) {
				h = Mathf.PerlinNoise (i, j) * Persistence;
				pos.x = i;
				pos.y = h;
				pos.z = j;
				CubeBuilder.CreateCube (_Parent.transform, pos);
			}
		}
	}

	private void SetTexture(Cube cube)
	{
		if (cube == null) {
			return;
		}
		Vector3 pos = cube.transform.position;
		if (pos.y > 0)
			cube.ReplaceTexture(ResourceManger.Instance.Get<Texture>(CubeTextures[0]));
		else if (pos.y == 0)
			cube.ReplaceTexture(ResourceManger.Instance.Get<Texture>(CubeTextures[1]));
		else
			cube.ReplaceTexture(ResourceManger.Instance.Get<Texture>(CubeTextures[2]));
	}
}
