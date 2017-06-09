using UnityEngine;
using System.Collections;

public class LayerCreater<T> where T : GroundLayer
{
	/// <summary>
	/// 地皮层
	/// </summary>
	private T _Layer;

	public T Layer {
		get { 
			return _Layer;
		}
	}

	public LayerCreater(T t)
	{
		_Layer = t;
	}
}

