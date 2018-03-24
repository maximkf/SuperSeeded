using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

	public float fadeAmount;
	public bool fadeOver = false;
	public bool destroyOnFadeOver;
	private float alpha = 2.0f;
	private Material material;
	private IEnumerator fade;

	void Start(){

		material = GetComponent<Renderer>().material;
	}

	// Update is called once per frame
	void Update () {
		if(fadeOver)
			StopCoroutine(fade);

		if(fadeOver && destroyOnFadeOver)
			Destroy(this.gameObject);
	}

	public void startFade(){
		fade = fadeOut();
		if(material != null)
				StartCoroutine(fade);
	}

	IEnumerator fadeOut(){
		while(alpha >= 0)
		{

			Color matCol = material.color;
			alpha -= fadeAmount;
			Color newColor = new Color(matCol.r, matCol.g, matCol.b, alpha);
			material.color = newColor;

			yield return null;
		}

		fadeOver = (alpha <= 0) ? true : false;
	}

}
