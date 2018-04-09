using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {


	// Update is called once per frame
	public void spawnObject (GameObject g) {
		GameObject go = Instantiate(g, transform.position, Random.rotation);
		go.transform.localScale = this.transform.localScale;
		GameManager.Instance.activePlayers.Add(go);
	}
}
