using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public int bumpCounter;
	public float moveSpeed, bumpForce, targetTimer, walkSoundRate;
	public bool autoTarget;
	public GameObject target;
	public Vector3 moveVelocity;

	private Vector3 moveInput, playerDirection, bumpVelocity, lastStep;
	private Player player, otherPlayer;
	private Rigidbody rigidbody;
	private float initDrag, yRange, targetTime, angryTime; //distanceToGround
	private GameObject groundPlane;
	// Use this for initialization

	void Start() {
		bumpCounter = 0;
		player = GetComponent<Player>();
		rigidbody = player.rigidbody;
		groundPlane = GameManager.Instance.groundPlane;
		initDrag = rigidbody.drag;
		yRange = rigidbody.position.y * 1.02f;
		lastStep = transform.position;
		rigidbody.AddTorque(0.1f,0.05f,0.1f, ForceMode.Impulse);
	}

	// Update is called once per frame
	void FixedUpdate () {
		targetTime += Time.deltaTime;

		float distanceToGround = rigidbody.position.y - groundPlane.transform.position.y;
		float distanceTraveled = Vector3.Distance(transform.position,lastStep);
		if(hasInput())
			doMove();

		if(distanceTraveled > walkSoundRate){
			lastStep = transform.position;
			player.playSound("walk", 1);
		}
	}

	void doMove(){
			rigidbody.drag = initDrag;
			moveVelocity = moveInput * moveSpeed;
			rigidbody.AddForce(moveVelocity, ForceMode.Acceleration);
	}

	public void wasBumped(Vector3 bumpForce){
		bumpCounter ++;
		rigidbody.AddForce(bumpForce, ForceMode.Impulse);
		// player.playSound("bump", bumpForce.magnitude * 0.3f);
	}

	void pickTarget(){
		float edgeDist = Vector3.Distance(transform.position, groundPlane.transform.position);
		moveInput = Vector3.Normalize(target.transform.position - transform.position);
		if(Random.Range(0.0f,1.0f) < 0.5f && edgeDist < 2.5f)
			moveInput = new Vector3(Mathf.Round(Random.Range(-1.0f,1.0f)), 0, Mathf.Round(Random.Range(-1.0f,1.0f)));
		else
			moveInput = new Vector3(Mathf.Round(moveInput.x), 0, Mathf.Round(moveInput.z));
		targetTime = 0.0f;
	}

	public bool hasInput(){
		if(!autoTarget)
			moveInput = player.input;
		else if(target != null && targetTime > targetTimer)
			pickTarget();

		playerDirection = Vector3.right * moveInput.x + Vector3.forward * moveInput.z;
		return (playerDirection.sqrMagnitude > 0.0f)? true : false;
	}

	// void OnTriggerEnter(Collider other){
	// 	string otherTag = other.gameObject.tag;
	// 	switch(otherTag){
	// 		case "Player" :
	// 			otherPlayer = other.gameObject.GetComponent<Player>();
	// 			if(rigidbody.velocity.magnitude > otherPlayer.rigidbody.velocity.magnitude)
	// 				GameManager.Instance.playerCollision(player.playerNum, otherPlayer.playerNum);
	// 		break;
	// 		default:
	// 		break;
  //
	// 	}
	// }

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, moveInput + transform.position);
	}

}
