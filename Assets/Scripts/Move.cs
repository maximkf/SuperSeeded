using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	private Rigidbody rigidbody;
	private float playerSpeed, dashSpeed;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		playerSpeed = GetComponent<PlayerData>().moveSpeed;
		dashSpeed = GetComponent<PlayerData>().dashSpeed;
	}

	// Update is called once per frame
	public void doMove (Vector3 direction) {
		Vector3 force = direction * playerSpeed;
		rigidbody.AddForce(force, ForceMode.Acceleration);
	}

	public void doDash (Vector3 direction) {
		Vector3 force = direction * dashSpeed;
		rigidbody.AddForce(force, ForceMode.Force);
	}
}
