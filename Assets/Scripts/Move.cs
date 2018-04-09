using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	private Rigidbody rigidbody;
	private float playerSpeed;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		playerSpeed = GetComponent<PlayerData>().moveSpeed;
	}

	// Update is called once per frame
	public void doMove (Vector3 direction) {
		Vector3 force = direction * playerSpeed;
		print(force);
		rigidbody.AddForce(force, ForceMode.Acceleration);
	}
}
