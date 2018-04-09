using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafEmitter : MonoBehaviour {

	public GameObject leafParticle;
	public int minPart, maxPart;
	public float minSpeed, maxSpeed, minScale, maxScale;
	private int leavesToMake;
	private List <GameObject> leaves= new List<GameObject>();
	private BoxCollider emitter;
	private Vector3 size;
	// Use this for initialization
	void Start () {
		leavesToMake = (int)Random.Range(minPart, maxPart);
		emitter = GetComponent<BoxCollider>();
		size = emitter.extents;
		MakeLeaves();
	}

	void MakeLeaves () {
		CleanUpLeaves();
		leaves.Clear();
		for(int i = 0; i < leavesToMake; i++){
			Vector3 startPosition = new Vector3(Random.Range(size.x, -size.x),
			Random.Range(size.y, -size.y),Random.Range(size.z, -size.z));
			Vector3 velocity = RandomVector.getRandomUnitVector();
			GameObject leaf = Instantiate(leafParticle, startPosition += transform.position, Random.rotation);
			leaf.GetComponent<Rigidbody>().velocity = velocity * Random.Range(minSpeed, maxSpeed);
			leaf.transform.localScale *= Random.Range(minScale, maxScale);
			leaf.transform.parent = this.transform;
		}
	}

	void CleanUpLeaves(){
		foreach(GameObject g in leaves){
			Destroy(g);
		}
	}
}
