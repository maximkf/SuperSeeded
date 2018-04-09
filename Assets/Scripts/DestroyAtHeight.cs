using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtHeight : MonoBehaviour {

	public float yHeight;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if(pos.y < yHeight);
			Destroy(this);
	}
}
