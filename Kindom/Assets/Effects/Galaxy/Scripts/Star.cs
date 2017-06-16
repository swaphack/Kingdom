using UnityEngine;
using System.Collections;

/// <summary>
/// 星星
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Star : MonoBehaviour
{
	/// <summary>
	/// 自转轴
	/// </summary>
	public Vector3 Axis;
	/// <summary>
	/// 自转速度
	/// </summary>
	public float Speed;
	/// <summary>
	/// 运动速度
	/// </summary>
	public float MoveSpeed;

	void Awake() {
		Rigidbody rigidbody = this.GetComponent<Rigidbody> ();
		rigidbody.useGravity = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		AutoRotate();
		AutoRevolute ();
	}

	/// <summary>
	/// 自转
	/// </summary>
	private void AutoRotate() {
		this.transform.Rotate (Axis, Speed * Time.deltaTime);
	}

	/// <summary>
	/// 公转
	/// </summary>
	private void AutoRevolute() {
		Galaxy galaxy = this.GetComponentInParent<Galaxy> ();
		if (galaxy == null) {
			return;
		}

		Vector3 force = galaxy.GetStarForce (this);
		force += Vector3.Normalize(this.transform.forward) * MoveSpeed;
		//force *= Time.deltaTime;
		this.GetComponent<Rigidbody> ().AddForce (force);
	}
}

