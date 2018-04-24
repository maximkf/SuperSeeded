using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBump : MonoBehaviour {

	public float bumpMultiplier, threshold;

	Vector3 velocity;
	Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
		rigidbody = this.GetComponent<Rigidbody>();
	}

	void FixedUpdate(){
		velocity = rigidbody.velocity;
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject != null && other.gameObject.tag == "Player" && velocity.magnitude > threshold){
			print(velocity.magnitude);
			Vector3 bumpForce = velocity * bumpMultiplier;
			other.gameObject.GetComponent<Rigidbody>().AddForce(bumpForce, ForceMode.Impulse);
		}
	}
}
