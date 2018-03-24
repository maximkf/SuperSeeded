using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

	public GameObject objectToFollow;

	private Vector3 offset;

	void Start(){
		offset = transform.position;
	}

	void Update(){
		if(objectToFollow != null)
		transform.position = objectToFollow.transform.position + offset;
	}

}
