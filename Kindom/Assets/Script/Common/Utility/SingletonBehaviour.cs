using UnityEngine;
using System.Collections;

namespace Common.Utility
{
	/// <summary>
	/// 单例行为
	/// </summary>
	public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
	{
		private static T s_Instance;

		public static T Instance {
			get { 
				return s_Instance;
			}
		}

		protected SingletonBehaviour ()
		{
			s_Instance = (T)this;
		}
	}

}