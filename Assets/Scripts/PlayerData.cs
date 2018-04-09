using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public Color playerColor;
	public int playerNum;
	public float moveSpeed, dashSpeed, bumpMagnitude;
	public bool lost;
	public PrefabBurst burstPrefab;

	private PlayerInput playerInput;
	private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
		if(GameManager.Instance.currentGameState.ToString() == "GameStart")
			GameSetup();
	}

	public void GameSetup(){
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.isKinematic = false;
		// rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
		playerInput = gameObject.AddComponent(typeof(PlayerInput)) as PlayerInput;
		playerInput.GameSetup(playerNum);
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.currentGameState.ToString() == "GameStart")
			checkRingEdge();
	}

	// void OnCollisionEnter(Collision c){
	// 	if(c.gameObject.tag == "Ground" && GameManager.Instance.currentGameState.ToString() == "GameStart"){
	// 		lost = true;
	// 		GameManager.Instance.findWinningPlayer(playerNum);
	// 	}
	// }

	void checkRingEdge(){
		var heading = Vector3.zero - transform.position;
		print(heading.sqrMagnitude);
		if (transform.position.y < 5 && heading.sqrMagnitude > 75f) {
			lost = true;
			GameManager.Instance.findWinningPlayer(playerNum);
			PrefabBurst pb = Instantiate(burstPrefab, transform.position, Quaternion.identity);
			pb.burstColor = playerColor;
			Destroy(this.gameObject);
		}
	}
}
