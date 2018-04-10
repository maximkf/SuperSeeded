using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public Color playerColor, altPlayerColor;
	public int playerNum;
	public float moveSpeed, dashSpeed, bumpMagnitude;
	public bool lost, hasInput;
	public GameObject deathPrefab;

	private PlayerInput playerInput;
	private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
		if(GameManager.Instance.currentGameState.ToString() == "GameReady")
			StartCoroutine("GameReady");
	}

	IEnumerator GameReady(){
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.isKinematic = false;
		// rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
		playerInput = gameObject.AddComponent(typeof(PlayerInput)) as PlayerInput;
		yield return new WaitForSeconds(3);
		playerInput.GameSetup(playerNum);
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.currentGameState.ToString() == "GameStart")
			checkRingEdge();
	}

	void checkRingEdge(){
		var heading = Vector3.zero - transform.position;
		if (transform.position.y < 5 && heading.sqrMagnitude > 90f) {
			lost = true;
			GameManager.Instance.findWinningPlayer(playerNum);
			// var burstPosition = new Vector3(transform.position.x, 2.78f, transform.position.z);
			GameObject g = Instantiate(deathPrefab, transform.position, Quaternion.Euler(90,0,0));
			var main = g.GetComponentInChildren<ParticleSystem>().main;
			main.startColor = new ParticleSystem.MinMaxGradient(playerColor, altPlayerColor);
			PlayerInput pInput = g.AddComponent(typeof(PlayerInput)) as PlayerInput;
			pInput.playerNum = playerNum;
			Destroy(this.gameObject);
		}
	}
}
