using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour {

	public int numberID;
	public float bumpMagnitude;
	public bool hasGraphic;
	private Vector3 bumpForce, bumpDirection;
	// Use this for initialization
	void Start () {
		//use this if the bump has a graphic
		if(hasGraphic){
			Quaternion bumpRotation = Quaternion.identity;
			bumpRotation.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
			transform.rotation = bumpRotation;
		}
		// transform.localScale *= bumpScale;
	}

	public void setBump(int num, float mag){
		numberID = num;
		bumpMagnitude = mag;
	}

	void OnTriggerEnter(Collider other){
    if(other.gameObject.tag == "Player" && numberID !=
		other.transform.parent.gameObject.GetComponent<PlayerData>().playerNum){
      doBump(other.transform.parent.gameObject, bumpMagnitude);
    }else if(other.gameObject.tag == "Particle"){
			doBump(other.gameObject, bumpMagnitude * 0.05f);
		}
  }

  void doBump(GameObject other, float mag){
			//a vector pointing from center to other object
      bumpDirection = new Vector3 (other.transform.position.x - transform.position.x,
      transform.position.y, other.transform.position.z - transform.position.z);
			float att = Vector3.Dot(bumpDirection, bumpDirection);
			float atten = 1.0f/(att+1);

			float distance = Mathf.Clamp(bumpDirection.magnitude, 2f, 10f);
			//bumpForce scaled to a set magnitude devided by distance sqr
      bumpForce = bumpDirection / distance;//normalized direction
      // bumpForce *= (mag * mag) / (distance * distance);
			bumpForce *= mag * atten;
			other.GetComponent<Rigidbody>().AddForce(bumpForce, ForceMode.Impulse);
      // print(bumpMagnitude - bumpDirection.magnitude * falloff);
      // player.moveScript.wasBumped(bumpForce);
  }
	void OnDrawGizmos(){
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, transform.position+bumpForce);
			Gizmos.DrawLine(transform.position, transform.position+bumpDirection);
	}
	//animate and trigger destroy script with event
}
