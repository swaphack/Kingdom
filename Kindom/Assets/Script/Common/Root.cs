using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HighlightingEffect))]
public class Root : MonoBehaviour
{
	void Awake() {
		AddGameObject<ResourceManger> ();
		AddGameObject<InputManager> ();
		AddGameObject<DeviceRoot> ();
	}

	void AddGameObject<T>() where T : Component
	{
		GameObject go = new GameObject ();
		go.AddComponent<T> ();
		go.name = typeof(T).ToString();
		go.transform.SetParent (this.transform);
	}
}

