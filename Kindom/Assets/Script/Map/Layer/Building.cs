using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑
/// </summary>
public class Building : GroundTile 
{
	internal class BuildingModel : GroundTile
	{
		void Start() {
			OuterGlowColor = Color.white;
			TouchEnable = true;
			ScrollListener.Instance.AddDispatch (this.gameObject, this.OnRotation);
		}

		void OnDestory() {
			ScrollListener.Instance.RemoveDispatch (this.gameObject);
		}

		/// <summary>
		/// 旋转
		/// </summary>
		/// <param name="direction">Direction.</param>
		private void OnRotation(Vector3 direction) {
			if (!IsTouched) {
				return;
			}
			this.transform.Rotate (new Vector3(0, -direction.x, 0));
		}

		/// <summary>
		/// 点击事件
		/// </summary>
		protected override void OnTouchEvent(Vector3 hitInfo) {
			if (IsTouched == false) {
				PlayHighlight ();
			} else {
				CancelHighlight ();
			}

			CameraBehaviour cb = Camera.main.GetComponent<CameraBehaviour> ();
			if (cb != null) {
				cb.EnableRotate = IsTouched;
			}

			IsTouched = !IsTouched;
		}
	}

	internal class BuildingUI
	{
		/*
		void Update() {
			Vector3 srcPos = this.transform.position;
			Vector3 destPos = Camera.main.transform.position;
			destPos.y = srcPos.y;

			Quaternion q = Quaternion.FromToRotation (srcPos, destPos);
			this.transform.rotation = q;
		}
		*/
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
	void Start () {
		TouchEnable = true;

		CreateModel ();
		CreateUI ();

		KeyboardListener.Instance.AddDispatch (this.gameObject, KeyCode.Delete, this.OnRemoveSelf);
	}

	void OnDestroy() {
		KeyboardListener.Instance.RemoveDispatch (this.gameObject, KeyCode.Delete);
	}

	private void CreateModel() {
		GameObject child = ResourceManger.Instance.CreateGameObject (BuildingPrefab);
		if (child == null) {
			return;
		}

		child.AddComponent<BuildingModel> ();

		Vector3 pos = Vector3.one;
		pos.x = TileSize.Width * 0.5f;
		pos.y = 0;
		pos.z = TileSize.Height * 0.5f;

		child.transform.position = pos;
		child.name = "Model";
		child.transform.SetParent (this.transform);
	}

	private void CreateUI() {
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

		Vector3 pos = Vector3.one;
		pos.x = TileSize.Width * 0.5f;
		pos.y = 12;
		pos.z = TileSize.Height * 0.5f;

		Vector3 scale = go.transform.localScale;
		scale.z = -scale.z;

		Quaternion q = Quaternion.Euler(270, 0, 0);

		go.name = "UI";
		go.transform.localScale = scale;
		go.transform.position = pos;
		go.transform.rotation = q;
		go.transform.SetParent (this.transform);

		HideUI ();
	}

	/// <summary>
	/// 点击事件
	/// </summary>
	protected override void OnTouchEvent(Vector3 hitInfo) {
		if (IsTouched == false) {
			ShowUI ();
		} else {
			HideUI ();
		}

		IsTouched = !IsTouched;
	}
	
	void OnRemoveSelf(TouchPhase touchPhase) {
		if (!IsTouched) {
			return;
		}
		GameObject.Destroy (this.gameObject);
	}

	public void ShowUI() {
		Transform transform = this.transform.Find ("UI");
		if (transform != null) {
			transform.gameObject.SetActive (true);
			return;
		}
	}

	public void HideUI() {
		Transform transform = this.transform.Find ("UI");
		if (transform != null) {
			transform.gameObject.SetActive (false);
			return;
		}
	}
}
