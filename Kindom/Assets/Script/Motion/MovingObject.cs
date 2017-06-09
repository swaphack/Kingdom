using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingObject : MonoBehaviour 
{
	/// <summary>
	/// 起始作用力
	/// </summary>
	public Vector3 Force;

	// Use this for initialization
	void Start () {
		Rigidbody rigidbody = GetComponent<Rigidbody> ();
		rigidbody.AddForce (Force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
