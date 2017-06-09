using UnityEngine;
using System.Collections;

public class UILayer : MonoBehaviour
{
	/// <summary>
	/// 根据名称路径查找控件
	/// name: Scroll View.xx.xx
	/// </summary>
	/// <returns>The control by name.</returns>
	/// <param name="name">Name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T FindControlByName<T>(string name) where T : UIControl {
		if (string.IsNullOrEmpty(name)) {
			return null;
		}
		string[] nameNodes = name.Split (',');
		if (nameNodes == null || nameNodes.Length == 0) {
			return null;
		}

		int i = 0;
		Transform last = this.transform;
		Transform child;
		do {
			child = last.Find(nameNodes[i]);
			if (child == null) {
				return null;
			}
			last = child;
			i++;
		} while (i < nameNodes.Length);

		if (child == null) {
			return null;
		}

		if (child.GetComponent<T> () == null) {
			T t = child.gameObject.AddComponent<T> ();
			t.Init ();
		}

		return child.GetComponent<T> ();
	}
}

