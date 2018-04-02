using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	public void doMove (Vector3 direction, float speed) {
		Vector3 force = direction * speed;
		rigidbody.AddForce(force, ForceMode.Force);
	}
}
