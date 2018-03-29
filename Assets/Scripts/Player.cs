using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Player : MonoBehaviour {

	public int playerNum;
	public float bumpMagnitude, bumpCooldown, bumpScale;
	// public GameObject[] model;
	// public Color[] color;
	public Color playerColor;
	public bool usingKeyboard, lost, moving;
	public InputDevice inputDevice;
	public Rigidbody rigidbody;
	public Vector3 input;
	public PrefabBurst burstPrefab;
	public Bump bumpPrefab;
	public Material titleMaterial;
	public AudioClip burstSound, bumpSound;
	public AudioClip[] playerWalks;
	public AudioSource audioSource;
	public Move moveScript;

	private int lastDeviceCount;
	private float bumpTimer;
	private string playerName;
	private bool ringOut, title, bumpInput;
	private GameObject readyIcon, bumpReadyIcon;

	void Start(){
		ringOut = false;
		lost = false;
		lastDeviceCount = InputManager.Devices.Count;
		moveScript = GetComponent<Move>();
	}

	void Update(){
		if(InputManager.Devices.Count != lastDeviceCount)
			getInputDevice();
		moving = moveScript.hasInput();

		// if(bumpReady() && readyIcon == null){
		// 	readyIcon = Instantiate(bumpReadyIcon, transform.position, Quaternion.Euler(0, Random.Range(0,360), 0));
		// }

	}

	void FixedUpdate () {

		if(!usingKeyboard){
			DeviceInput();
		}else{
			KeyboardInput();
		}
		if(bumpReady() && bumpInput)
			createBump();

		if(lost)
			isDead();
	}

	public void setupPlayer(int i, bool isTitle){
		rigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		// bumpReadyIcon = GameManager.Instance.bumpReadyIcon;
		if(bumpPrefab == null)
			bumpPrefab = GameManager.Instance.bumpPrefab;
		playerNum = i;
		playerName = gameObject.name.Replace("(Clone)", "");
		// print(playerName);
		// if(isTitle){
		// 	foreach(Renderer m in GetComponentsInChildren<Renderer>())
		// 		m.material = titleMaterial;	//TODO: replace with all white texture rather than material?
		// 	playerColor = Color.white;}
		title = isTitle;
		getInputDevice();
	}

	public void getInputDevice(){
		if(playerNum < InputManager.Devices.Count){
			inputDevice = InputManager.Devices[playerNum];
			usingKeyboard = false;
		}else{
			// inputDevice = null;
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
				input = new Vector3(Input.GetAxisRaw("Horizontal2"), 0, Input.GetAxisRaw("Vertical2")) * 0.65f;
				bumpInput = Input.GetKeyDown(KeyCode.LeftShift);
				break;
			case 1:
				input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 0.65f;
				bumpInput = Input.GetKeyDown(KeyCode.RightShift);
				break;
			default:
				break;
		}
		// if(Input.GetKeyDown("return"))
		// 	print("pause");
	}

	void DeviceInput(){
		input = new Vector3(inputDevice.Direction.X, 0.0f, inputDevice.Direction.Y);
		bumpInput = inputDevice.Action1.WasPressed;
		// if(inputDevice.MenuWasPressed)
		// 	print("pause");//do something in game manager to update based on game state
	}

	public void playSound(string sound, float volume){
		audioSource.pitch = Random.Range(0.7f,1.3f);
		switch(sound){
			case "bump":
				if(bumpSound == null)
					GameManager.Instance.playerBumpSound(playerNum);
				audioSource.volume = Mathf.Clamp01(volume);
				audioSource.PlayOneShot(bumpSound);
				break;
			case "walk":
				audioSource.volume = Mathf.Clamp01(volume);
				audioSource.PlayOneShot(playerWalks[Random.Range(0,playerWalks.Length)]);
				break;
			default:
				break;
		}
	}

	void createBump(){
		bumpTimer = bumpCooldown;
		playSound("bump", bumpMagnitude);
		if(readyIcon != null)
			Destroy(readyIcon);
		Vector3 bumpLocation = new Vector3(transform.position.x, -0.6f, transform.position.z);
		Bump bump = Instantiate(bumpPrefab, bumpLocation, Quaternion.identity);
		bump.playerNum = playerNum;
		bump.bumpMagnitude = bumpMagnitude;
		bump.bumpScale = bumpScale;
	}

	bool bumpReady(){
		bumpTimer -= Time.deltaTime;
		return(bumpTimer <= 0)? true: false;
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
			AudioSource burstAudio = burst.gameObject.GetComponent<AudioSource>();
			burstAudio.pitch = Random.Range(0.8f,1.3f);
			burstAudio.PlayOneShot(burstSound);
			burst.monoColor = title;
			burst.burstColor = playerColor;
		}
		Destroy(this.gameObject);
	}

	void OnDestroy(){
		moveScript.bumpCounter = 0;
		if(readyIcon != null)
			Destroy(readyIcon);
	}
}
