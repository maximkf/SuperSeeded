using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOut : MonoBehaviour {

	public bool didCross;
	public Color colorA, colorB;
	public float colorLerp;
	public int flashCount;
	public Player p1, p2;

	private Material material;
	private Collider collider;
	private IEnumerator ringFlash;
	public GameObject groundPlane;
	// private GameObject losingPlayer;
	// private Player loser;

	// Use this for initialization
	void Start () {
		material = GetComponent<MeshRenderer>().material;
		material.color = colorA;
		collider = GetComponent<Collider>();
		ringFlash = RingFlash();
	}

	public void setPlayers(){

	}

	public void lerpRingColor(){
		if(!didCross){
			float distance1 = Vector3.Distance(p1.transform.position, groundPlane.transform.position);
			float distance2 = Vector3.Distance(p2.transform.position, groundPlane.transform.position);
			float newDistance = Mathf.Max(distance1, distance2) - 3.0f;
			material.color = Color.Lerp(colorA, colorB, newDistance/3.7f);
		}
	}

	void OnTriggerExit(){
		ringFlash = RingFlash();
		didCross = true;
		collider.enabled = false;
		StartCoroutine(ringFlash);
	}

	public void resetRing(Player _p1, Player _p2){
		print("reset");
		p1 = _p1;
		p2 = _p2;
		groundPlane = GameManager.Instance.groundPlane;
		StopCoroutine(ringFlash);
		material.color = colorA;
		collider.enabled = true;
		didCross = false;
	}

	IEnumerator RingFlash(){
		int i = 0;
		while(i <= flashCount)
		{
			material.color = colorA;
			yield return new WaitForSeconds(0.1f);
			material.color = colorB;
			i ++;
			yield return new WaitForSeconds(0.1f);
		}
		yield return null;
	}
}
