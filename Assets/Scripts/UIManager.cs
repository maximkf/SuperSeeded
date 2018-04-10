using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

	public Renderer[] backgrounds;
	public Carousel[] carousels;
	public Animator[] icons;
	public int totalSelected;
	public bool countDownOver, fadeOver;
	public Text winText, endCountText;
	public Text[] selectedText;
	// private Canvas gameCanvas;
	private Animator canvasAnimator;

	private bool[] playersSelected  = {false, false};

	public void countDown(int bit){
		if(canvasAnimator == null)
			canvasAnimator = GetComponent<Animator>();

		switch(bit){
			case 0:
				countDownOver = false;
				canvasAnimator.SetTrigger("Flash");
				canvasAnimator.SetTrigger("CountDown");
			break;
			case 1:
				countDownOver = true;
			break;
			case 2:
				countDownOver = false;
				canvasAnimator.SetTrigger("SelectCountDown");
			break;
			// case 3:
			// 	canvasAnimator.SetBool("Rematch", true);
			default:
			break;
		}
	}

	public void displayWin(GameObject winningPlayer,int waitTime){
		countDown(2);
		StartCoroutine(countDownCoroutine(waitTime));
		winText.text = winningPlayer.name.Replace("(Clone)","") + " Wins!";
	}

	IEnumerator countDownCoroutine(int t){
		int count = t;

		while (count > 0){
			endCountText.text = count.ToString();
			yield return new WaitForSeconds(1);
			count --;
			yield return null;
		}
		if(count == 0)
			countDownOver = true;
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
		selectedText[num].text = carousels[num].selectedObjectName.Replace("(Clone)","");
	}

	public void selectPlayer(int num){
		UpdateSelectIcons(num);
		GameManager.Instance.playersToSpawn[num]
		= carousels[num].objects[carousels[num].selectedObject];
	}

	void UpdateSelectIcons(int num){
		if(!playersSelected[num]){
			totalSelected ++;
			icons[num].SetBool("selected", true);
			playersSelected[num] = true;
		}
	}

	public void timeOut(int bit){
		if(canvasAnimator == null)
			canvasAnimator = GetComponent<Animator>();

		if(bit == 0){
			fadeOver = false;
			canvasAnimator.SetTrigger("FadeOut");
		}
		if(bit == 1)
			fadeOver = true;
	}
}
