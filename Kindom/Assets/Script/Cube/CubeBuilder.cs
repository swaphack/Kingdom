using UnityEngine;
using System.Collections;

/// <summary>
/// 方块创建者
/// </summary>
public class CubeBuilder
{
	/// <summary>
	/// 创建方块
	/// </summary>
	public static Cube CreateCube(Transform parent, Vector3 position)
	{
		Cube cube = CreateCube ();
		cube.transform.position = position;
		if (parent != null) {
			cube.transform.SetParent (parent);
		}
		return cube;
	}

	/// <summary>
	/// 创建方块
	/// </summary>
	public static Cube CreateCube()
	{
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Renderer render = go.GetComponent<Renderer> ();
		if (render != null) {
			render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			render.receiveShadows = false;
		}

		Cube newCube = go.AddComponent<Cube> ();
		newCube.Init ();

		return newCube;
	}
}

