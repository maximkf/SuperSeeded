using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerSelect : MonoBehaviour {


	public void SwapPlayer(string direction, int num){
		if(direction == "Right")
			UIManager.Instance.swapPlayer(num, direction);
		else if(direction == "Left")
			UIManager.Instance.swapPlayer(num, direction);
	}

	public void SelectPlayer(bool selection, int num){
		if(selection)
			UIManager.Instance.selectPlayer(num);
	}
}
