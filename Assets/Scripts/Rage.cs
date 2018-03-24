using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : MonoBehaviour {

	public bool angry;
	public float angryTimer, bumpBoost, moveBoost;
	public GameObject angerSymbol;
	public Vector3 angerSpriteOffset;

	private float angryTime, ogBumpMag, ogMoveSpeed, ogBumpScale;
	private Move moveScript;
	private Player player;
	private GameObject angrySign;
	// private Quaternion angryRotation;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
		moveScript = GetComponent<Move>();

		// ogBumpMag = player.bumpMagnitude;
		ogMoveSpeed = moveScript.moveSpeed;
		// ogBumpScale = player.bumpScale;
	}

	// Update is called once per frame
	void Update () {

		if(angry){
				angryTime += Time.deltaTime;
		}

		if(angryTime > angryTimer && angry){
			// player.bumpMagnitude = ogBumpMag;
			moveScript.moveSpeed = ogMoveSpeed;
			// moveScript.bumpCounter = 0;
			Destroy(angrySign.gameObject);
			angry = false;
		}

		// if(moveScript.bumpCounter >= 3 && !angry){
		// 	angrySign = Instantiate(angerSymbol, angerSpriteOffset, Quaternion.Euler(90,0,0));
		// 	angrySign.GetComponent<FollowObject>().objectToFollow = player.gameObject;
		// 	player.bumpMagnitude *= bumpBoost;
		// 	player.bumpScale *= bumpBoost;
		// 	moveScript.moveSpeed *= moveBoost;
		// 	angry = true;
		// }
	}

	void OnDestroy(){
		if(angrySign != null)
			Destroy(angrySign.gameObject);
	}
}
