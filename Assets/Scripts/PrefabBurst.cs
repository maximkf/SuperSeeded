using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBurst : MonoBehaviour {

	public float minVelocity, maxVelocity, scaleMax, colorOffset, fadeAmount;
	public int minParticles, maxParticles;
	public GameObject particlePrefab;
	public bool burst, monoColor;
	public Color burstColor;
	public bool timerEnded = false;
	public float burstTimer;

	private float burstTime;
	private int particlesToSpawn;
	private List<GameObject> particles = new List<GameObject>();

	void Start(){
		burst = true;
		burstTime = 0;
		particlesToSpawn = Random.Range(minParticles, maxParticles);
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

			Vector3 direction = RandomVector.getRandomUnitVector();
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
