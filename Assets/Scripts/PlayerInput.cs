using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour {

	public int playerNum;
	public bool hasInput;
	// public bool usingKeyboard;
	private InputDevice inputDevice;
	private int lastDeviceCount;
	private bool selectScene, gameOn;
	private float dashAngle;
	private PlayerSelect playerSelect;
	private Move moveScript;
	private Dash dashScript;
	// private Grow growScript;
	private Vector3 inputDirection;
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
		dashScript = GetComponent<Dash>();
		// growScript = GetComponent<Grow>();
		gameOn = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
		checkForInput();

		if(InputManager.Devices.Count != lastDeviceCount)
			getInputDevice();

		if(inputDevice != null && selectScene)
			UpdatePlayerSelect();

		if(inputDevice != null && gameOn)
			UpdatePlayerActions();

		if(!UIManager.Instance.countDownOver && GameManager.Instance.currentGameState.ToString()
		== "GameEnd" && inputDevice.Action1.WasPressed)
			GameManager.Instance.rematch();
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
		inputDirection = Vector3.right * inputDevice.Direction.X
				+ Vector3.forward * inputDevice.Direction.Y;
		if(!dashScript.dash){
		// }
			moveScript.doMove(inputDirection);
		}
		if(inputDevice.Action1.WasPressed && inputDirection.sqrMagnitude > 0.1f){
			// growScript.startGrow();
			dashScript.doDash(inputDirection);
		}

		// if(dashScript.dash && inputDevice.Direction.HasChanged){
		// 	print("canceled dash");
		// 	dashScript.doBreak();
		// }
	}

	void checkForInput(){
		if(inputDevice != null && !inputDevice.AnyButton.IsPressed && !inputDevice.DPad.IsPressed
		&& !inputDevice.Direction.HasChanged){
			hasInput = false;
		}else if(inputDevice == null){
			hasInput = false;
		}else{
			hasInput = true;
		}

		GameManager.Instance.hasInput = hasInput;
	}

	// bool changedDirection(){
	// 	if(dashAngle - getInputAngle() > 0.5f){
	// 		return true;
	// 	}else{
	// 		return false;
	// 	}
	// }
	//
	// float getInputAngle(){
	// 	Vector3 targetDir = inputDirection - transform.position;
	// 	float angle = Vector3.Angle(targetDir, transform.forward);
	// 	return angle;
	// }

}
