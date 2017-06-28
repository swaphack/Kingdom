using UnityEngine;
using System.Collections;

namespace Geography.Map
{
	/// <summary>
	/// 背景
	/// </summary>
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
	public class Background : MonoBehaviour
	{
		protected MeshFilter meshFilter;
		protected MeshRenderer meshRenderer;
		protected MeshCollider meshCollider;

		void Start() {
			this.Init ();
		}

		protected virtual void Init() {
			meshFilter = this.GetComponent<MeshFilter> ();
			meshRenderer = this.GetComponent<MeshRenderer> ();
			meshCollider = this.GetComponent<MeshCollider> ();
		}
	}
}