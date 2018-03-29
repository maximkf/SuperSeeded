using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


	public AudioClip ringOutSound;
	public float winClipLength;
	public AudioClip[] winAudio;
	public AudioClip[] playerAudio;
	public AudioClip[] bumpAudio;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void playWinSequence(bool toggle){
		if(toggle)
			InvokeRepeating("randomAudioSequence", 0, winClipLength);//play random audio at fixed interval

		if(!toggle)
			CancelInvoke("randomAudioSequence");
	}

	void randomAudioSequence(){
		// audioSource.clip = winAudio[Random.Range(0,winAudio.Length)];
		audioSource.PlayOneShot(winAudio[Random.Range(0,winAudio.Length)]);
	}

	public AudioClip getBumpClip(){
		return bumpAudio[Random.Range(0,bumpAudio.Length)];
	}
}
