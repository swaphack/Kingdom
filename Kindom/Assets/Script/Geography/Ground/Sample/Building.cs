using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geography.Ground;

namespace Geography.Ground.Sample
{

	/// <summary>
	/// 建筑
	/// </summary>
	public class Building : Tile
	{
		internal class BuildingModel : ModelBehaviour
		{
			void Start ()
			{
				OuterGlowColor = Color.white;
				EnableTouch = true;
				AddScrollListener ();
			}

			void OnDestory ()
			{
				RemoveScrollListener ();
			}

			public void AddScrollListener ()
			{
				ScrollListener.Instance.AddDispatch (this.gameObject, this.OnRotation);
			}

			public void RemoveScrollListener ()
			{
				ScrollListener.Instance.RemoveDispatch (this.gameObject);
				if (Camera.main == null) {
					return;
				}
				CameraBehaviour cb = Camera.main.GetComponent<CameraBehaviour> ();
				if (cb != null) {
					cb.EnableRotate = true;
				}
			}

			/// <summary>
			/// 旋转
			/// </summary>
			/// <param name="direction">Direction.</param>
			private void OnRotation (TouchPhase touchPhase, Vector3 direction)
			{
				if (!IsTouched) {
					return;
				}
				this.transform.Rotate (new Vector3 (0, -direction.x, 0));

				CameraBehaviour cb = Camera.main.GetComponent<CameraBehaviour> ();
				if (cb == null) {
					return;
				}
				if (touchPhase == TouchPhase.Moved) {
					cb.EnableRotate = false;
				} else {
					cb.EnableRotate = true;
				}
			}

			/// <summary>
			/// 点击事件
			/// </summary>
			public override bool OnTouchModel (Vector3 hitInfo)
			{
				if (IsTouched == false) {
					PlayHighlight ();
				} else {
					CancelHighlight ();
				}

				IsTouched = !IsTouched;

				return true;
			}
		}

		internal class BuildingUI
		{
		}

		/// <summary>
		/// 建筑预制体
		/// </summary>
		public string BuildingPrefab = "Prefabs/Medieval_Building_19";
		/// <summary>
		/// 标记材质
		/// </summary>
		public string FlagMatUrl = "Materials/FlagMat";
		
		// Use this for initialization
		void Start ()
		{
			CreateModel ();
			CreateUI ();

			KeyboardListener.Instance.AddDispatch (this.gameObject, KeyCode.Delete, this.OnRemoveSelf);
		}

		void OnDestroy ()
		{
			KeyboardListener.Instance.RemoveDispatch (this.gameObject, KeyCode.Delete);
			this.GetComponentInChildren<BuildingModel> ().RemoveScrollListener ();
		}

		private void CreateModel ()
		{
			GameObject child = ResourceManger.Instance.CreateGameObject (BuildingPrefab);
			if (child == null) {
				return;
			}

			child.AddComponent<BuildingModel> ();
			child.transform.position = this.transform.position;
			child.transform.localScale = Constants.GetBuildingScale (TileSize, TileCount);
			child.name = "Model";
			child.transform.SetParent (this.transform);

			ObstacleBehaviour ob = child.AddComponent<ObstacleBehaviour> ();
			ob.Center = new Vector3 (0, TileSize.Height * 0.25f, 0);
			ob.Size = new Vector3 (TileSize.Width * 0.5f, TileSize.Height * 0.5f, TileSize.Height * 0.5f);
		}

		private void CreateUI ()
		{
			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Plane);
			if (go == null) {
				return;
			}

			string url = "Textures/Event/Watch";

			Material mat = ResourceManger.Instance.Get<Material> (FlagMatUrl);
			if (mat != null) {
				mat.mainTexture = ResourceManger.Instance.Get<Texture2D> (url);
				MeshRenderer meshRender = go.GetComponent<MeshRenderer> ();
				meshRender.material = mat;
			}

			Collider collider = go.GetComponent<Collider> ();
			collider.enabled = false;

			Vector3 pos = this.transform.position;
			pos.y += 12;

			//Vector3 scale = go.transform.localScale;
			//scale.z = -scale.z;

			Quaternion q = Quaternion.Euler (270, 0, 0);

			go.name = "UI";
			//go.transform.localScale = scale;
			go.transform.position = pos;
			go.transform.rotation = q;
			go.transform.SetParent (this.transform);

			HideUI ();
		}

		/// <summary>
		/// 点击事件
		/// </summary>
		public override bool OnTouchModel (Vector3 touchPosition)
		{
			if (IsTouched == false) {
				ShowUI ();
			} else {
				HideUI ();
			}

			IsTouched = !IsTouched;

			return true;
		}

		void OnRemoveSelf (TouchPhase touchPhase)
		{
			if (!IsTouched) {
				return;
			}
			GameObject.Destroy (this.gameObject);
		}

		public void ShowUI ()
		{
			Transform transform = this.transform.Find ("UI");
			if (transform != null) {
				transform.gameObject.SetActive (true);
				return;
			}
		}

		public void HideUI ()
		{
			Transform transform = this.transform.Find ("UI");
			if (transform != null) {
				transform.gameObject.SetActive (false);
				return;
			}
		}
	}

}