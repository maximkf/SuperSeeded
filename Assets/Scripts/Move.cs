using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float moveSpeed;

	private Rigidbody rigidbody;
	private Dash dashScript;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		dashScript = GetComponent<Dash>();
	}

	// Update is called once per frame
	public void doMove (Vector3 direction) {
		Vector3 force = direction * moveSpeed;
		rigidbody.AddForce(force, ForceMode.Acceleration);
	}

	// public void doBump(Vector3 force){
	// 	rigidbody.AddForce(force, ForceMode.Impulse);
	// }
}
