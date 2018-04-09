using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public Color playerColor;
	public int playerNum;
	public float moveSpeed, bumpMagnitude;
	public bool lost;
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

	}
}
