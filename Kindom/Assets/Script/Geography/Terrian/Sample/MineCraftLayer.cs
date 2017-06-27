using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geography.Terrian;

namespace Geography.Terrian.Sample
{
	public class MineCraftLayer : UILayer
	{
		/// <summary>
		/// 随机种子
		/// </summary>
		public int Seed = 4541;
		/// <summary>
		/// 宽度
		/// </summary>
		public int Width = 20;
		/// <summary>
		/// 长度
		/// </summary>
		public int Length = 20;
		/// <summary>
		/// 持久度
		/// </summary>
		[Range (0, 32)]
		public float Persistence = 3;
		/// <summary>
		/// 倍数
		/// </summary>
		[Range (1, 10)]
		public int Octaves = 5;
		/// <summary>
		/// 方块纹理
		/// </summary>
		public string[] CubeTextures = new string[] {
			"Textures/blocks/carpet/carpet_colored_black",
			"Textures/blocks/carpet/carpet_colored_blue",
			"Textures/blocks/carpet/carpet_colored_brown",
			"Textures/blocks/carpet/carpet_colored_cyan",
			"Textures/blocks/carpet/carpet_colored_gray",
			"Textures/blocks/carpet/carpet_colored_green",
			"Textures/blocks/tallgrass/1",
			"Textures/blocks/tallgrass/2",
			"Textures/blocks/tallgrass/3",
			"Textures/blocks/tallgrass/4",
			"Textures/blocks/tallgrass/5",
			"Textures/blocks/tallgrass/6",
		};

		/// <summary>
		/// 透明纹理
		/// </summary>
		public string TransparentTexture = "Textures/blocks/transparent";
		/// <summary>
		/// 父节点
		/// </summary>
		private GameObject _Parent = null;
		// Use this for initialization
		void Start ()
		{
			_Parent = new GameObject ();
		}
	}

}