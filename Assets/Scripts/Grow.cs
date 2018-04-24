using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

	public float growTime, growMultiplier, growCoolDown;
	public GameObject bumpPrefab;
	private PlayerData playerData;
	private int playerNum;
	private bool bumped;
	private Vector3 originalScale, alteredScale;
	private float deltaGrowTime, growRate, yHeight, coolDown;
	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
		alteredScale = originalScale;
		playerData = GetComponent<PlayerData>();
	}

	void FixedUpdate(){
		//find the fraction of multiplier in total growtime over change in time
		growRate = growTime/growTime * Time.deltaTime;

		if(deltaGrowTime > growTime * 0.5f && !bumped){
			// Invoke("MakeBump",0);
		}

		coolDown -= Time.deltaTime;
	}

	public void startGrow(){
		if(coolDown <= 0f){
			deltaGrowTime = 0;
			yHeight = transform.position.y;
			StartCoroutine("DoGrow");
		}
	}

	IEnumerator DoGrow(){
		coolDown = growCoolDown;
		Invoke("MakeBump",0);
		while(deltaGrowTime < growTime){
			deltaGrowTime += growRate;//advance total growth;
			float growAmount = (growMultiplier - 1f) * (deltaGrowTime/growTime);
			alteredScale = originalScale + originalScale * growAmount;
			transform.localScale = alteredScale;
			yield return null;
		}
		ResetScale();
		yield break;
	}

	void MakeBump(){
		bumped = true;
		var bumpPlaneHeight = playerData.ringSize.y * 0.5f;
		Vector3 bumpLocation = new Vector3(transform.position.x, bumpPlaneHeight, transform.position.z);
		GameObject bump = Instantiate(bumpPrefab, bumpLocation, Quaternion.identity);
		bump.GetComponent<Bump>().setBump(playerData.playerNum,playerData.bumpMagnitude);
	}

	void ResetScale(){
		bumped = false;
		transform.localScale = originalScale;
		alteredScale = originalScale;
		transform.position = new Vector3(transform.position.x, yHeight, transform.position.z);
	}
}
