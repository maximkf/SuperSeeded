using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public float speed = 1;
	public float dashImpulse;
	private Dash dashScript;
	private Move moveScript;
	private Vector3 moveInput;
	private bool actionInput;

	// Use this for initialization
	void Start () {
		moveScript = GetComponent<Move>();
		dashScript = GetComponent<Dash>();
	}

	// Update is called once per frame
	void Update () {
		keyboardInput();
		if(actionInput)
			dashScript.doDash(moveInput, dashImpulse);
		else
			moveScript.doMove(moveInput, speed);
	}

	void keyboardInput(){
		moveInput = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		actionInput = Input.GetKeyDown(KeyCode.Space);
	}
}
