using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtHeight : MonoBehaviour {

	public float yHeight;

	// Update is called once per frame
	void Update () {
		if(transform.position.y < yHeight)
			Destroy(this.gameObject);
	}
}
