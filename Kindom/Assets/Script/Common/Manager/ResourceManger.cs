using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManger : Singleton<ResourceManger>
{
	private Dictionary<string, Object> _ResItems;

	private ResourceManger()
	{
		_ResItems = new Dictionary<string, Object> ();
	}

	/// <summary>
	/// 加载资源
	/// </summary>
	/// <param name="url">URL.</param>
	public bool Load<T>(string url) where T : Object
	{
		if (string.IsNullOrEmpty (url)) {
			return false;
		}
		if (_ResItems.ContainsKey (url)) {
			return true;
		}

		T go = Resources.Load<T> (url);
		if (go == null) {
			return false;
		}
		_ResItems.Add (url, go);
		return true;
	}

	/// <summary>
	/// 加载实例
	/// </summary>
	/// <param name="url">URL.</param>
	public T Get<T>(string url) where T : Object
	{
		if (string.IsNullOrEmpty (url)) {
			return null;
		}
		if (_ResItems.ContainsKey (url)) {
			return (T)_ResItems [url];
		}

		if (!Load<T> (url)) {
			return null;
		}

		return Get<T> (url);
	}


	/// <summary>
	/// 加载资源
	/// </summary>
	/// <param name="url">URL.</param>
	public bool LoadGameObject(string url) {
		return Load<GameObject> (url);
	}

	/// <summary>
	/// 加载实例
	/// </summary>
	/// <param name="url">URL.</param>
	public GameObject CreateGameObject(string url) {
		return Object.Instantiate<GameObject>(Get<GameObject> (url));
	}
}

