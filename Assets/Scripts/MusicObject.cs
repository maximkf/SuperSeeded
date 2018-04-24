using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObject : MonoBehaviour {

	public AudioClip music;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		audioSource.loop = true;
		if (music != null)
		{
			audioSource.clip = music;
			audioSource.Play();
		}

		DontDestroyOnLoad (this);
	}
}
