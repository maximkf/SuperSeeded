using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

	public AudioClip[] playerWalks;
	public AudioClip bumpSound, deathSound;
	public AudioClip[] endMusic;
	public AudioClip titleMusic, menuMusic;
	public AudioClip menuClick, menuSelect, menuCancel;
	private AudioSource audioSource;
	private List<AudioClip> audioPool = new List<AudioClip>();
	private bool LoopStarted;
	private MusicObject musicObject;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		foreach(AudioClip ac in audioPool){
			audioSource.PlayOneShot(ac, Random.Range(0.2f,1));
			audioPool.Remove(ac);
		}

	}

	public void addToAudioPool(AudioClip ac){
		audioPool.Add(ac);
	}

	public void backgroundMusic(AudioClip music){
		if(musicObject != null)
			Destroy(musicObject);

		GameObject go = Instantiate(gameObject);
		musicObject = go.AddComponent(typeof(MusicObject))as MusicObject;
		musicObject.music = music;
	}
}
