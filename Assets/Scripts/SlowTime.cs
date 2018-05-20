using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour {

	public float slowAmount;
	public float duration;
	public bool timeReset = false;

	private float gameTimeScale, slowTimer;
	// Use this for initialization
	void Start () {
		gameTimeScale = slowAmount;
		// inverseSlowAmount = 1 / slowAmount;
		slowTimer = 0;
		updateTimeScale(gameTimeScale);
		GameManager.Instance.slowTime(gameTimeScale);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(slowTimer < duration)
			slowTimer += Time.unscaledDeltaTime;

		if(!timeReset && slowTimer >= duration){
			gameTimeScale = 1;
			updateTimeScale(gameTimeScale);
			timeReset = true;
		}
	}

	void updateTimeScale(float t){
			GameManager.Instance.slowTime(t);
	}
}
