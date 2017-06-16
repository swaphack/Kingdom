using UnityEngine;
using System.Collections;

public class Galaxy : MonoBehaviour
{
	/// <summary>
	/// 万有引力常数 6.67 * 10^-7
	/// 0.000000667f
	/// </summary>
	public const float GRAVIATION = 0.667f;


	public static Vector3 GetForce(Rigidbody src, Rigidbody dest) {
		if (src == null || dest == null) {
			return Vector3.zero;
		}
		Vector3 direction = src.transform.position - dest.transform.position;
		float distance2 = Vector3.SqrMagnitude (direction);
		float g = GRAVIATION * src.mass * dest.mass / distance2;
		return g * direction;
	}

	public static Vector3 GetForce(Rigidbody[] srcs, Rigidbody dest) {
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

