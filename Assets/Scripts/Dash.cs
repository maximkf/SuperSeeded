using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

	public GameObject partA;
	public GameObject partB;
	public GameObject puff;
	public float splitDelay;
	public float breakDelay;

	private Rigidbody rigidbody;
	private GameObject newPartB;
	private bool split, dash;
	private float delay;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();//get rigidbody
	}

	void FixedUpdate(){
		if(split)
			delay -= Time.deltaTime;//decriment delay

		// rigidbody.drag = (rigidbody.velocity.sqrMagnitude) * 0.1f;
	}

	// Update is called once per frame
	public void doDash (Vector3 direction, float magnitude) {
		Vector3 force = direction * magnitude;//force to apply

		if(!split)//if not split
		{
			rigidbody.AddForce(force, ForceMode.Impulse);//apply force
			rigidbody.drag = 0;
			dash = true;
			Invoke("Break", breakDelay);
			// splitPlayer(direction);//split player
		}
	}

	void splitPlayer(Vector3 direction){
		newPartB = Instantiate(partB, transform.position, Quaternion.identity);//make a partB
		if(puff != null)//if a puff prefab exists
			Instantiate (puff, transform.position, Quaternion.identity);//make a puff
		// newPartB.name = gameObject.name + "Part";//name after fruit prefab
		partB.SetActive(false);//turn off the partB in original prefab
		//TODO: depending on how this looks replace with swaping whole for new active part
		Rigidbody rb = newPartB.AddComponent(typeof(Rigidbody)) as Rigidbody;//add reigid body to part
		rb.AddForce(direction * -1f, ForceMode.Impulse);
		//give new part a random vector with a random magnitude
		delay = splitDelay; //reset split delay for time to wait before merge
		split = true; //split is true and function exits
	}

	void OnTriggerEnter(Collider other){
		if(split && other.gameObject == newPartB && delay <= 0){
			//if split, matches part, and delay elapsed
			Destroy(other.gameObject);//destroy part
			partB.SetActive(true);//reactivate the part in gameObject TODO:see above
			split = false;// reset split
		}
	}

	void Break(){
		rigidbody.drag = 0.5f;
		rigidbody.velocity *= 0f;//stop motion
	}

}
