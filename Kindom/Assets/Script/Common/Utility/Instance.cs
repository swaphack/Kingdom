using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static T s_Instance;

	public static T Instance {
		get { 
			return s_Instance;
		}
	}

	protected Singleton()
	{
		s_Instance = (T)this;
	}
}

