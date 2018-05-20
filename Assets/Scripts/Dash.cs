using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

	// public GameObject partA;
	// public GameObject partB;
	// public GameObject puff;
	// public float splitDelay;
	// public float breakDelay;
	public float dashSpeed, dashTime, bumpMagnitude;
	public bool dash;
	public Bump bumpObject;
	public GameObject dashParticle;

	private List <GameObject> particleSystems = new List<GameObject>();
	private Rigidbody rigidbody;
	private GameObject newPartB;
	private Vector3 dashForce;
	private bool split;
	private float delay, dashTimer;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();//get rigidbody

	}

	void FixedUpdate(){
		// if(split)
		// 	delay -= Time.deltaTime;//decriment delay

		// rigidbody.drag = (rigidbody.velocity.sqrMagnitude) * 0.1f;
	}

	// Update is called once per frame
	public void doDash (Vector3 direction) {
		dashForce = direction * dashSpeed;//force to apply
		dash = true;
		StartCoroutine("DashRoutine");
		// if(!split)//if not split
		// {
		// 	rigidbody.AddForce(force, ForceMode.Impulse);//apply force
		// 	rigidbody.drag = 0;
		// 	dash = true;
		// 	Invoke("Break", breakDelay);
		// 	// splitPlayer(direction);//split player
		// }
	}

	IEnumerator DashRoutine(){
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
		Vector3 startPosition = transform.position;
		rigidbody.AddForce(dashForce, ForceMode.Impulse);
		//create a particle system to render dash trail and add to list of systems
		GameObject dPart = Instantiate(dashParticle, transform.position, Quaternion.identity);
		dPart.transform.parent = gameObject.transform;
		particleSystems.Add(dPart);
		// bool dashStarted = false;
		print("dashRoutine started");//loop check
		while(dash){
			//add force to rigidbody
			// if(!dashStarted){
			//  rigidbody.AddForce(dashForce, ForceMode.Impulse);
			// 	dashStarted = true;
			// }
			//find the distance between starPos and transform;
			// Vector3 dashElapsed = new Vector3(startPosition.x - transform.position.x, transform.position.y, startPosition.z - transform.position.z);
			//update clock
			dashTimer ++;
			//check if the distance traveled or time equals thresh and exit through bool
			if(dashTimer >= dashTime){
				doBreak();
				dashTimer = 0;
			}
			yield return null;
		}
		rigidbody.constraints = RigidbodyConstraints.None;
		yield break;
	}

	// void OnTriggerEnter(Collider other){
	// 	//if dashing and hit another player
	// 	if(dash && other.gameObject.tag == "Player"){
	// 		//apply velocity to other player rigidbody
	// 		Vector3 bumpForce = rigidbody.velocity.normalized * bumpMagnitude;
	// 		other.gameObject.GetComponentInParent<Rigidbody>().AddForce(bumpForce, ForceMode.Impulse);
	//
	// 		//create a bump object
	// 		Bump b = Instantiate(bumpObject, transform.position, Quaternion.identity);
	//
	// 		doBreak();
	// 		//pass player number to bumpObject
	// 	}
	// }

	public void doBreak(){
		// rigidbody.drag = 0.5f;
		foreach(GameObject go in particleSystems){
			if(go != null && go.transform.parent != null)
				go.transform.parent = null;
		}
		rigidbody.velocity *= 0.8f;//decrease velocity
		if(dash)
			dash = false;
	}

	void OnDestroy(){
		foreach(GameObject go in particleSystems){
			Destroy(go);
		}
	}

}
