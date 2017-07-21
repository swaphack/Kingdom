using UnityEngine;
using System.Collections;
using Common.Utility;

namespace Geography.Map
{
	/// <summary>
	/// 区域
	/// </summary>
	public class Area : Background
	{
		/// <summary>
		/// 顶点坐标
		/// </summary>
		public Vector2[] Vectices;
		/// <summary>
		/// 颜色
		/// </summary>
		public Color Color;

		protected string matUrl = "Materials/AreaMat";
		
		public override void Initialize() {
			base.Initialize ();

			Material mat = ResourceManger.Instance.Get<Material> (matUrl);
			meshRenderer.material = mat;

			Mesh2D.CreateVecticeMesh (meshFilter, Vectices);
			Mesh2D.CreateColorMaterial (meshRenderer, Color);
			meshCollider.sharedMesh = meshFilter.mesh;
		}
	}
}