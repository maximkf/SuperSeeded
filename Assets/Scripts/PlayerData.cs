using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public Color playerColor, altPlayerColor;
	public int playerNum;
	public float bumpMagnitude;
	public bool lost, hasInput;
	public GameObject deathPrefab, dashCollider;
	public Vector3 ringSize;

	private PlayerInput playerInput;
	private Rigidbody rigidbody;
	private Vector3 yPlaneZero;
	// Use this for initialization
	void Start () {
		if(GameManager.Instance.currentGameState.ToString() == "GameReady")
			StartCoroutine("GameReady");
	}

	IEnumerator GameReady(){
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.isKinematic = false;
		// GameObject dCol = Instantiate(dashCollider, transform.position, Quaternion.identity);
		// dCol.transform.parent = this.gameObject.transform;
		// rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
		playerInput = gameObject.AddComponent(typeof(PlayerInput)) as PlayerInput;
		yield return new WaitForSeconds(3);
		yPlaneZero = new Vector3(0, transform.position.y, 0);;
		playerInput.GameSetup(playerNum);
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.currentGameState.ToString() == "GameStart")
			checkRingEdge();
	}

	void checkRingEdge(){
		var distFromCenter = yPlaneZero - transform.position;
		float ringRadius = ringSize.x * 0.5f;

		if (transform.position.y < 5 && distFromCenter.sqrMagnitude > ringRadius * ringRadius) {
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
