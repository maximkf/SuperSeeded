using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

	public float growTime, growMultiplier;
	public bool grow;
	public GameObject bumpPrefab;
	private PlayerData playerData;
	private int playerNum;
	private bool bumped;
	private Vector3 originalScale, alteredScale;
	private float deltaGrowTime, growRate, yHeight;
	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
		alteredScale = originalScale;
		playerData = GetComponent<PlayerData>();
	}

	void FixedUpdate(){
		//find the fraction of multiplier in total growtime over change in time
		growRate = growTime/growTime * Time.deltaTime;
		//while room to grow
		if(grow && growTime < deltaGrowTime){
			resetScale();

		}

		if(!grow){
			yHeight = transform.position.y;
		}else{
			doGrow();
		}
	}

	void doGrow(){
		deltaGrowTime += growRate;//advance total growth;
		float growAmount = (growMultiplier - 1f) * (deltaGrowTime/growTime);
		alteredScale = originalScale + originalScale * growAmount;
		transform.localScale = alteredScale;
		if(deltaGrowTime > growTime * 0.5f && !bumped){
			Vector3 bumpLocation = new Vector3(transform.position.x, 0.8f, transform.position.z);
			GameObject bump = Instantiate(bumpPrefab, bumpLocation, Quaternion.identity);
			bump.GetComponent<Bump>().setBump(playerData.playerNum,playerData.bumpMagnitude);
			bumped = true;
		}
	}

	void resetScale(){
		grow = false;
		bumped = false;
		deltaGrowTime = 0f;
		transform.localScale = originalScale;
		alteredScale = originalScale;
		transform.position = new Vector3(transform.position.x, yHeight, transform.position.z);
	}
}
