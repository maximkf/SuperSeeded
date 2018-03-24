using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

	public float bumpScale, actionChargeRate, chargeLimit;
	public bool actionInput;
	[HideInInspector]
	public int playerNum;
	[HideInInspector]
	public GameObject playerParticle;
	[HideInInspector]
	public PrefabBurst burstPrefab;
	[HideInInspector]
	public Color playerColor;

	private Bump bumpPrefab;
	private bool activated;
	private float actionCharge, initYHeight;
	private Vector3 initScale, bumpLocation;
	// Use this for initialization
	void Start () {
		if(bumpPrefab == null)
			bumpPrefab = GameManager.Instance.bumpPrefab.GetComponent<Bump>();
			initScale = transform.localScale;
			initYHeight = transform.position.y + 0.005f;
	}

	void FixedUpdate(){
		if(!actionInput)
			activated = false;

		if(!activated)
		 calculateCharge();
	}

	void calculateCharge(){
		if(actionInput && actionCharge < chargeLimit){
			actionCharge += actionChargeRate;
			float chargeToScale = actionCharge * 0.01f;
			float chargeScale = 1.0f + chargeToScale * chargeToScale;
			transform.localScale *= chargeScale;
		}else if (!actionInput  && actionCharge > 0 || actionInput && actionCharge >= chargeLimit){
			bump(actionCharge);
			actionCharge = 0;
			Invoke("resetScale", 0.1f);
			activated = true;
		}
	}

	void resetScale(){
			transform.localScale = initScale;
	}

	void bump(float magnitude){
	//	playSound("bump", bumpMagnitude);//TODO: move this to a collision detection function in move
		bumpLocation = new Vector3(transform.position.x, 0, transform.position.z);
		Bump bump = Instantiate(bumpPrefab, bumpLocation, Quaternion.identity);//TODO: add this back in at some later point
		// PrefabBurst burst = Instantiate(burstPrefab, this.transform.position, Quaternion.identity);
		// burst.gameObject.GetComponent<AudioSource>().enabled = false;
		// burst.burstColor = playerColor;
		bump.playerNum = playerNum;
		bump.bumpMagnitude = magnitude;
		bump.bumpScale = bumpScale * (magnitude * 0.1f);
	}
}
