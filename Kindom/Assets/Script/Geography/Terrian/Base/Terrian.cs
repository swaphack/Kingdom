using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Geography.Terrian
{
	/// <summary>
	/// 地形
	/// </summary>
	public class Terrian : MonoBehaviour, IStep
	{
		/// <summary>
		/// 地形数据
		/// </summary>
		private TData _TerrianData;
		/// <summary>
		/// 加载的索引
		/// </summary>
		private int _LoadCusor;

		public TData Data {
			get { 
				return _TerrianData;
			}
		}

		/// <summary>
		/// 是否结束
		/// </summary>
		public bool Finish { 
			get {
				return _LoadCusor >= _TerrianData.CubeDatas.Count;	
			} 
		}

		public Terrian()
		{
			_TerrianData = new TData ();

			StepManager.Instance.AddStep (this);
		}

		/// <summary>
		/// 获取纹理
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="index">Index.</param>
		private Texture GetTexture(int index) {
			if (index < 0 || _TerrianData.TextureDatas.Count <= index) {
				return null;
			}

			string filepath = _TerrianData.TextureDatas [index].Filepath;
			if (string.IsNullOrEmpty (filepath)) {
				return null;
			}

			return ResourceManger.Instance.Get<Texture> (filepath);
		}

		/// 创建方块
		/// </summary>
		private Cube CreateCube (CubeData data)
		{
			//GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
			GameObject go = new GameObject ();

			Cube newCube = go.AddComponent<Cube> ();
			newCube.Init ();
			newCube.transform.position = data.Position;
			newCube.transform.SetParent (this.transform);

			newCube.ReplaceTexture (Cube.CubeSide.Top, GetTexture(data.TopTexture));
			newCube.ReplaceTexture (Cube.CubeSide.Bottom, GetTexture(data.BottomTexture));
			newCube.ReplaceTexture (Cube.CubeSide.Left, GetTexture(data.LeftTexture));
			newCube.ReplaceTexture (Cube.CubeSide.Right, GetTexture(data.RightTexture));
			newCube.ReplaceTexture (Cube.CubeSide.Front, GetTexture(data.FrontTexture));
			newCube.ReplaceTexture (Cube.CubeSide.Back, GetTexture(data.BackTexture));

			Renderer render = go.GetComponent<Renderer> ();
			if (render != null) {
				render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
				render.receiveShadows = false;
			}

			return newCube;
		}

		/// <summary>
		/// 执行事件
		/// </summary>
		public void DoEvent() {
			if (_LoadCusor < 0 || _LoadCusor >= _TerrianData.CubeDatas.Count) {
				return;
			}
			CubeData data = _TerrianData.CubeDatas [_LoadCusor];

			this.CreateCube (data);

			_LoadCusor++;
		}
	}
}



