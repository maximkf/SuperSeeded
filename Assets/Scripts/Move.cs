using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float moveSpeed, targetTimer, walkSoundRate;
	public bool autoTarget, dash, actionInput;
	public GameObject target;
	public AudioClip[] playerWalks;

	private Vector3 playerDirection, lastStep, moveVelocity;
	private Rigidbody rigidbody;
	private float initDrag, yRange, targetTime, angryTime; //distanceToGround
	private GameObject groundPlane;
	private AudioSource audioSource;
	// Use this for initialization

	void Start() {
		rigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		groundPlane = GameManager.Instance.groundPlane;
		lastStep = transform.position;
		rigidbody.AddTorque(0.1f,0.05f,0.1f, ForceMode.Impulse);
	}

	// Update is called once per frame
	void FixedUpdate () {
		// targetTime += Time.deltaTime;

		float distanceToGround = rigidbody.position.y - groundPlane.transform.position.y;
		float distanceTraveled = Vector3.Distance(transform.position,lastStep);

		if(distanceTraveled > walkSoundRate){
			lastStep = transform.position;
			audioSource.pitch = Random.Range(0.7f,1.3f);
			audioSource.PlayOneShot(playerWalks[Random.Range(0,playerWalks.Length)]);
		}
	}

	public void doMove(Vector3 moveInput){
			// rigidbody.drag = initDrag;
			if(!actionInput)
				moveVelocity = moveInput * moveSpeed;
			else
				moveVelocity = moveInput * moveSpeed * 0.7f;

			rigidbody.AddForce(moveVelocity, ForceMode.Acceleration);
	}

	// void pickTarget(){
	// 	float edgeDist = Vector3.Distance(transform.position, groundPlane.transform.position);
	// 	moveInput = Vector3.Normalize(target.transform.position - transform.position);
	// 	if(Random.Range(0.0f,1.0f) < 0.5f && edgeDist < 2.5f)
	// 		moveInput = new Vector3(Mathf.Round(Random.Range(-1.0f,1.0f)), 0, Mathf.Round(Random.Range(-1.0f,1.0f)));
	// 	else
	// 		moveInput = new Vector3(Mathf.Round(moveInput.x), 0, Mathf.Round(moveInput.z));
	// 	targetTime = 0.0f;
	// }
}
