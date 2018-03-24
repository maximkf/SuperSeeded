using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Player : MonoBehaviour {

	public int playerNum;
	public Color playerColor;
	public bool usingKeyboard, lost, moving;
	public InputDevice inputDevice;
	public Rigidbody rigidbody;
	public GameObject deathParticle, bumpParticle;

	private Move moveScript;
	private PlayerAction playerAction;

	private int lastDeviceCount;
	private string playerName;
	private bool ringOut, title, actionInput;
	private Vector3 moveInput;
	private PrefabBurst burstPrefab;

	void Start(){
		lastDeviceCount = InputManager.Devices.Count;
		moveScript = GetComponent<Move>();
		playerAction = GetComponent<PlayerAction>();
		burstPrefab = GameManager.Instance.burstPrefab.GetComponent<PrefabBurst>();
		playerAction.burstPrefab = burstPrefab;
		playerAction.playerNum = playerNum;
		playerAction.playerColor = playerColor;
		// ringOut = false;
		// lost = false;
	}

	void Update(){
		if(InputManager.Devices.Count != lastDeviceCount)
			getInputDevice();
	}

	void FixedUpdate () {

		if(!usingKeyboard){
			DeviceInput();
		}else{
			KeyboardInput();
		}

		if(moveInput.sqrMagnitude > 0.0f){
			moving = true;
			moveScript.doMove(moveInput);
		}else{
			moving = false;
		}
			moveScript.actionInput = actionInput;

		playerAction.actionInput = actionInput;

		if(lost)
			isDead();
	}

	public void setupPlayer(int i, bool isTitle){
		rigidbody = GetComponent<Rigidbody>();

		playerNum = i;
		playerName = gameObject.name.Replace("(Clone)", "");
		getInputDevice();
	}

	public void getInputDevice(){
		if(playerNum < InputManager.Devices.Count){
			inputDevice = InputManager.Devices[playerNum];
			usingKeyboard = false;
		}else{
			usingKeyboard = true;
		}

		GameManager.Instance.playersConnected = InputManager.Devices.Count;
		lastDeviceCount = InputManager.Devices.Count;
	}

	void KeyboardInput()
	{
		switch(playerNum)
		{
			case 0:
				moveInput = new Vector3(Input.GetAxisRaw("Horizontal2"), 0, Input.GetAxisRaw("Vertical2")) * 0.65f;
				actionInput = Input.GetKey(KeyCode.LeftShift);
				break;
			case 1:
				moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 0.65f;
				actionInput = Input.GetKey(KeyCode.Space);
				break;
			default:
				break;
		}
		// if(Input.GetKeyDown("return"))
		// 	print("pause");
	}

	void DeviceInput(){
		moveInput = new Vector3(inputDevice.Direction.X, 0.0f, inputDevice.Direction.Y);
		actionInput = inputDevice.Action1.WasPressed;
		// if(inputDevice.MenuWasPressed)
		// 	print("pause");//do something in game manager to update based on game state
	}

	public void wasBumped(Vector3 bumpForce){
		// bumpCounter ++;
		rigidbody.AddForce(bumpForce, ForceMode.Impulse);
	}

	void OnTriggerExit(Collider other){
		if(!ringOut && other.gameObject.tag == "Ring"){
			print(playerName + " " + playerNum + " lost");
			GameManager.Instance.onGameEnd(this);
			ringOut = true;
		}
	}

	public void isDead(){
		if(lost){
			PrefabBurst burst = Instantiate(burstPrefab, this.transform.position, Quaternion.identity);
			// burst.monoColor = title;
			burst.particlePrefab = deathParticle;//TODO: replace with fruit slices
			burst.burstColor = playerColor;
		}
		Destroy(this.gameObject);
	}
}
