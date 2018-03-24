using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBurst : MonoBehaviour {

	public float minVelocity, maxVelocity, scaleMax, colorOffset, fadeAmount;
	public int minParticles, maxParticles;
	public GameObject particlePrefab, defaultParticle;
	public bool burst, monoColor;
	public Color burstColor;
	public AudioClip burstSound;
	public bool timerEnded = false;
	public float burstTimer;

	private float burstTime;
	private int particlesToSpawn;
	private List<GameObject> particles = new List<GameObject>();

	void Start(){
		burst = true;
		burstTime = 0;
		particlesToSpawn = Random.Range(minParticles, maxParticles);
		AudioSource burstAudio = GetComponent<AudioSource>();
		burstAudio.pitch = Random.Range(0.8f,1.3f);
		burstAudio.PlayOneShot(burstSound);

		if(particlePrefab == null)
			particlePrefab = defaultParticle;
	}

	// Update is called once per frame
	void Update () {
		updateParticleList();
		//pick a random number of particles to generate
		if(burst)
			spawnParticles();

		if(!timerEnded)
			burstTime += Time.deltaTime;

		if(!burst && burstTime >= burstTimer && !timerEnded){
			foreach(GameObject p in particles)
				p.GetComponent<FadeOut>().startFade();
			timerEnded = true;
		}

	}

	void spawnParticles(){

		for(int i = 0; i <= particlesToSpawn; i++){
			//for each particle pick a random prefab to Instantiate each with a random direction and velocity

			Vector3 direction = getRandomVector();
			direction *= Random.Range(minVelocity, maxVelocity);
			Quaternion particleRotation = Random.rotation;

			GameObject particle = Instantiate(particlePrefab, transform.position, particleRotation) as GameObject;
			Material particleMaterial = particle.GetComponent<Renderer>().material;

			if(!monoColor)
				particleMaterial.color = getNewColor(burstColor);
			else
				particleMaterial.color = burstColor;

			particle.GetComponent<FadeOut>().fadeAmount = Random.Range(0.1f, fadeAmount);
			particle.transform.parent = this.transform;
			particle.transform.localScale *= Random.Range(0.05f, scaleMax);
			particle.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);

			if(i == particlesToSpawn)
				burst = false;
		}
	}

	Vector3 getRandomVector(){
		Vector3 newVector = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f));

		return newVector;
	}

	Color getNewColor(Color color){
		Color colorRandom = new Color(Random.Range(-colorOffset, colorOffset),
		Random.Range(-colorOffset, colorOffset),Random.Range(-colorOffset, colorOffset));
		color += colorRandom;

		return color;
	}

	void updateParticleList(){
		particles.Clear();
		foreach(Transform child in this.transform) {
			particles.Add(child.gameObject);
		}

		if(timerEnded && particles.Count == 0)
			Destroy(this.gameObject);
	}

}
