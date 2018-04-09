using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

	public Renderer[] backgrounds;
	public Carousel[] carousels;
	public Animator[] icons;
	public int totalSelected;
	public bool countDownOver;
	private Canvas canvasGame;

	private bool[] playersSelected  = {false, false};

	public void countDown(int bit){
		if(canvasGame == null)
			canvasGame = GameObject.Find("Canvas").GetComponent<Canvas>();

		if(bit == 0){
			countDownOver = false;
			canvasGame.GetComponent<Animator>().SetTrigger("CountDown");
		}else if(bit == 1){
			countDownOver = true;
		}
	}

	public void displayRematch(){

	}

	public void swapPlayer(int num, string direction){
		if(playersSelected[num]){
			totalSelected --;
			icons[num].SetBool("selected", false);
			playersSelected[num] = false;
		}
		carousels[num].rotate(direction);
		icons[num].SetTrigger(direction);
	}

	public void setBackgroundColor(int num, GameObject focusObject){
		PlayerData pData = focusObject.GetComponent<PlayerData>();
		backgrounds[num].material.color = pData.playerColor;
	}

	public void selectPlayer(int num){
		UpdateSelectIcons(num);
		GameManager.Instance.playersToSpawn[num]
		= carousels[num].objects[carousels[num].selectedObject];
	}

	void UpdateSelectIcons(int num){
		print("updateIcons" + num);
		if(!playersSelected[num]){
			totalSelected ++;
			icons[num].SetBool("selected", true);
			playersSelected[num] = true;
		}
	}
}
