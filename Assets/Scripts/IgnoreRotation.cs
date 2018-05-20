using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreRotation : MonoBehaviour {

	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = Quaternion.identity;
	}
}
