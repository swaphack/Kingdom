using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common.Utility;

namespace Geography.Terrian
{
	/// <summary>
	/// 纹理数据
	/// </summary>
	public struct TextureData
	{
		/// <summary>
		/// 纹理路径
		/// </summary>
		public string Filepath;
	}

	/// <summary>
	/// 地块数据
	/// </summary>
	public struct CubeData
	{
		/// <summary>
		/// 位置坐标
		/// </summary>
		public Vector3 Position;
		/// <summary>
		/// 前面纹理索引
		/// </summary>
		public int FrontTexture;
		/// <summary>
		/// 背面纹理索引
		/// </summary>
		public int BackTexture;
		/// <summary>
		/// 顶部纹理索引
		/// </summary>
		public int TopTexture;
		/// <summary>
		/// 底部纹理索引
		/// </summary>
		public int BottomTexture;
		/// <summary>
		/// 左边纹理索引
		/// </summary>
		public int LeftTexture;
		/// <summary>
		/// 右边纹理索引
		/// </summary>
		public int RightTexture;
	}

	/// <summary>
	/// 地形数据
	/// </summary>
	public class TData
	{
		/// <summary>
		/// 纹理数据
		/// </summary>
		private List<TextureData> _TextureDatas;
		/// <summary>
		/// 方块数据
		/// </summary>
		private List<CubeData> _CubeDatas;

		/// <summary>
		/// 纹理数据
		/// </summary>
		/// <value>The texture datas.</value>
		public List<TextureData> TextureDatas {
			get { 
				return _TextureDatas;
			}
		}

		/// <summary>
		/// 方块数据
		/// </summary>
		/// <value>The cube datas.</value>
		public List<CubeData> CubeDatas {
			get { 
				return _CubeDatas;
			}
		}

		public TData() {
			_TextureDatas = new List<TextureData> ();
			_CubeDatas = new List<CubeData> ();
		}

		public void Clear() 
		{
			_TextureDatas.Clear ();
			_CubeDatas.Clear ();
		}

		/// <summary>
		/// 加载纹理数据
		/// </summary>
		/// <param name="filepath">Filepath.</param>
		public void LoadTextureDataFromFile(string filepath)
		{
			if (string.IsNullOrEmpty (filepath)) {
				return;
			}

			_TextureDatas.Clear ();

			byte[] datas = ResourceManger.Instance.GetBytes (filepath);
			if (datas == null || datas.Length == 0) {
				return;
			}

			ByteReader reader = new ByteReader (datas);
			int count = reader.Read<int> ();
			for (int i = 0; i < count; i++) {
				TextureData data = new TextureData ();
				data.Filepath = reader.ReadString ();
				_TextureDatas.Add (data);
			}
		}

		/// <summary>
		/// 加载方块数据
		/// </summary>
		/// <param name="filepath">Filepath.</param>
		public void LoadCubeDataFromFile(string filepath)
		{
			if (string.IsNullOrEmpty (filepath)) {
				return;
			}

			_CubeDatas.Clear ();

			byte[] datas = ResourceManger.Instance.GetBytes (filepath);
			if (datas == null || datas.Length == 0) {
				return;
			}

			ByteReader reader = new ByteReader (datas);
			int count = reader.Read<int> ();
			for (int i = 0; i < count; i++) {
				CubeData data = new CubeData ();
				data.Position = reader.ReadVector3 ();
				data.FrontTexture = reader.Read<int> ();
				data.BackTexture = reader.Read<int> ();
				data.TopTexture = reader.Read<int> ();
				data.BottomTexture = reader.Read<int> ();
				data.LeftTexture = reader.Read<int> ();
				data.RightTexture = reader.Read<int> ();
				_CubeDatas.Add (data);
			}
		}
	}

}