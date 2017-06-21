using UnityEngine;
using System.Collections;

/// <summary>
/// 星系
/// </summary>
public class Galaxy : MonoBehaviour
{
	/// <summary>
	/// 万有引力常数 6.02 * 10^-11
	/// 0.000000667f
	/// </summary>
	public float GRAVIATION = 10f;

	/// <summary>
	/// 获取作用力，src对dest的作用力
	/// </summary>
	/// <returns>The force.</returns>
	/// <param name="src">Source.</param>
	/// <param name="dest">Destination.</param>
	private Vector3 GetForce(Rigidbody src, Rigidbody dest) {
		if (src == null || dest == null) {
			return Vector3.zero;
		}
		Vector3 direction = src.transform.position - dest.transform.position;
		float distance2 = Vector3.SqrMagnitude (direction);
		float g = GRAVIATION * src.mass * dest.mass / distance2;
		return g * direction.normalized;
	}

	/// <summary>
	/// 获取作用力，srcs对dest的作用力
	/// </summary>
	/// <returns>The force.</returns>
	/// <param name="srcs">Srcs.</param>
	/// <param name="dest">Destination.</param>
	private Vector3 GetForce(Rigidbody[] srcs, Rigidbody dest) {
		if (srcs == null || srcs.Length == 0 || dest == null) {
			return Vector3.zero;
		}

		Vector3 force = Vector3.zero;

		for (int i = 0; i < srcs.Length; i++) {
			if (srcs [i] == null || srcs [i] == dest) {
				continue;
			} else {
				force += GetForce (srcs [i], dest);
			}
		}

		return force;
	}

	/// <summary>
	/// 获取星系引力
	/// </summary>
	/// <returns>The star force.</returns>
	/// <param name="star">Star.</param>
	public Vector3 GetStarForce(Star star) {
		Rigidbody[] children = this.GetComponentsInChildren<Rigidbody> ();
		if (children == null || children.Length == 0) {
			return Vector3.zero;
		}

		return GetForce (children, star.GetComponent<Rigidbody> ());
	} 
}

