﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEditor.Animations;

public class UIManager : MonoBehaviour {


	public GameObject startPanel;
	private GameObject canvas;
	public GameObject[] playerInfo;
	public List<GameObject> keyboardInfo = new List<GameObject>();
	public List<GameObject> gamepadInfo = new List<GameObject>();
	public float fadeTimer = 10;
	public Animator bumpAnimator;

	private Animator animator;
	private float fadeValue;
	private float fadeTime;

	void Start(){
		Initialize();
	}

	void Initialize(){
		// if(playerInfo == null)
			canvas = GameObject.Find("Canvas");
			animator = canvas.GetComponent<Animator>();
			animator.SetFloat("fade", 1.0f);
			fadeTime = 11.0f;
			playerInfo = GameObject.FindGameObjectsWithTag("PlayerUI");
			foreach(GameObject g in playerInfo){
				if(g.name == "Keyboard")
					keyboardInfo.Add(g);
				if(g.name == "Gamepad")
					gamepadInfo.Add(g);
			}
	}

	void Update(){

	}

	public void updateDirections(int i){
		int gamepads = i;
		if(playerInfo[0] != null)
			{
				switch (gamepads){
				case 0:
					gamepadInfo[0].SetActive(false);
					gamepadInfo[1].SetActive(false);
					keyboardInfo[0].SetActive(true);
					keyboardInfo[1].SetActive(true);
					break;
				case 1:
					gamepadInfo[0].SetActive(false);
					gamepadInfo[1].SetActive(true);
					keyboardInfo[0].SetActive(true);
					keyboardInfo[1].SetActive(false);
					break;
				case 2:
					gamepadInfo[0].SetActive(true);
					gamepadInfo[1].SetActive(true);
					keyboardInfo[0].SetActive(false);
					keyboardInfo[1].SetActive(false);
					break;
				default:
					break;
			}
		}
	}

	public void fadeDirections(bool movement){


		if(movement){
			fadeValue -= 0.02f;
			fadeTime = 0;
		}else{
			fadeTime += Time.deltaTime;
		}

		if(!movement && fadeTime > fadeTimer){
			fadeValue += 0.02f;
			bumpAnimator.SetBool("Toggle", true);
		}

		if(fadeValue < 0){
			fadeValue = 0;
			bumpAnimator.SetBool("Toggle", false);
		}

		if(fadeValue > 1)
			fadeValue = 1;

		animator.SetFloat("fade", fadeValue);
	}

	public void reset(){
		gamepadInfo.Clear();
		keyboardInfo.Clear();
		Initialize();
	}




	// public void ChangeTitleColor(Color color, int index, string playerName){
	// 	if(index < titleText.Length){
	// 		titleText[index].color = color;
	// 		titleText[index].text = playerName.Replace("(Clone)", "");
	// 	}
	// }
  //
	// public void updateDeviceText(bool ready){
  //
	// 	switch (GameManager.Instance.playersConnected)
	// 	{
	// 		case 0:
	// 			foreach(Text t in playerText)
	// 				t.color = Color.grey;
	// 			break;
	// 		case 1:
	// 			playerText[0].color = Color.white;
	// 			playerText[1].color = Color.grey;
	// 			break;
	// 		case 2:
	// 			foreach(Text t in playerText)
	// 				t.color = Color.white;
	// 				break;
	// 		default:
	// 			break;
	// 	}
  //
	// 	startPanel.SetActive(ready);
	// 	ready = !ready;
	// 	connectPanel.SetActive(ready);
	// }
}
