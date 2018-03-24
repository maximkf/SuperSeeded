using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

	public float bumpThreshold = 0.8f;
	public GameObject bumpObject;
	public GameObject groundPlane;

	// Use this for initialization
	void Start () {

	}

	// public void handlePlayerCollision(Player stronger, Player weaker){
	// 	Vector3 bumpVelocity = stronger.rigidbody.velocity;
	// 	float bumpMagnitude = bumpVelocity.magnitude;
	// 	float counterMagnitude = weaker.rigidbody.velocity.magnitude;
	//
	// 	if(bumpMagnitude - counterMagnitude >= bumpThreshold){
	// 		weaker.moveScript.wasBumped(bumpVelocity);
	// 		Vector3 midPoint = Vector3.Lerp(stronger.transform.position, weaker.transform.position, 0.5f);
	// 		Vector3 bumpLocation = new Vector3(midPoint.x, groundPlane.transform.position.y, midPoint.z);
	// 		createBump(bumpLocation, bumpMagnitude);
	// 	}
	// }
	//
	// void createBump(Vector3 location, float scaleFactor){
	// 	Quaternion bumpRotation = Quaternion.identity;
	// 	bumpRotation.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
	// 	GameObject b = Instantiate(bumpObject, location, bumpRotation) as GameObject;
	// 	b.transform.localScale *= scaleFactor;
	// }
			// isAngry = false;
		// }else{
			//add to player bump ANGRY MECHANIC

			// bumpCounter ++;
      //
			// if(!isAngry && bumpCounter > 2 && angrySign == null){
			// 	angryTime = 0;
			// 	angrySign = Instantiate(angerSymbol, angryOffset, Quaternion.Euler(90,0,0));
			// 	angrySign.GetComponent<FollowObject>().objectToFollow = this.gameObject;
			// 	angrySign.GetComponent<FadeOut>().startFade();
			// 	angerBoost = 1.4f;
			// 	bumpForce += 0.8f;
			// 	isAngry = true;
		// 	}
		// }
}
