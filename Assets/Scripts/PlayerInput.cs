using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour {

	public int playerNum;
	// public bool usingKeyboard;
	private InputDevice inputDevice;
	private int lastDeviceCount;
	private bool selectScene, gameOn;
	private PlayerSelect playerSelect;
	private Move moveScript;
	private Vector3 moveDirection;
	// Use this for initialization
	void Start () {
		// selectScene = false;
		// gameOn = false;
		getInputDevice();

		if(GameManager.Instance.currentGameState.ToString() == "PlayerSelect"){
			playerSelect = GetComponent<PlayerSelect>();
			selectScene = true;
		}

		lastDeviceCount = InputManager.Devices.Count;
	}

	public void GameSetup(int num){
		playerNum = num;
		getInputDevice();
		moveScript = GetComponent<Move>();
		gameOn = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(InputManager.Devices.Count != lastDeviceCount)
			getInputDevice();

		if(selectScene)
			UpdatePlayerSelect();

		if(gameOn)
			UpdatePlayerActions();
	}

	public void getInputDevice(){
		if(playerNum < InputManager.Devices.Count){
			inputDevice = InputManager.Devices[playerNum];
		// 	usingKeyboard = false;
		// }else{
		// 	inputDevice = null;
		// 	usingKeyboard = true;
		}
		GameManager.Instance.playersConnected = InputManager.Devices.Count;
		lastDeviceCount = InputManager.Devices.Count;
	}

	void UpdatePlayerSelect(){
		if(inputDevice.DPadRight.WasPressed || inputDevice.RightBumper.WasPressed
			|| inputDevice.LeftStick.Right.WasPressed)
			playerSelect.SwapPlayer("Right", playerNum);
		else if(inputDevice.DPadLeft.WasPressed || inputDevice.LeftBumper.WasPressed
			|| inputDevice.LeftStick.Left.WasPressed)
			playerSelect.SwapPlayer("Left", playerNum);
		else if(inputDevice.Action1.WasPressed)
			playerSelect.SelectPlayer(true, playerNum);
	}

	void UpdatePlayerActions(){
		// if(inputDevice.LeftStick.HasChanged || inputDevice.DPad.HasChanged ){
			// print("check");
			moveDirection = Vector3.right * inputDevice.Direction.X
			+ Vector3.forward * inputDevice.Direction.Y;
		// }
		moveScript.doMove(moveDirection);
	}

}
