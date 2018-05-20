using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour {

	public GameObject target;

  private Vector3 bumpDirection;

	// Use this for initialization
	void Start () {

	}

	void Update(){

	}

  void doBump(){

  }

	void OnDrawGizmos(){
			Gizmos.color = Color.red;
			//
			// Gizmos.DrawLine(bumpOrigin, transform.position+bumpForce);
	}

}
