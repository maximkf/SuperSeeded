using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {


	// Update is called once per frame
	public void spawnObject (GameObject g, int pNum, MeshCollider ring) {
		GameObject go = Instantiate(g, transform.position, Random.rotation);
		PlayerData pd = go.GetComponent<PlayerData>();
		pd.playerNum = pNum;
		pd.ringSize = ring.bounds.size;
		go.transform.localScale = this.transform.localScale;
		GameManager.Instance.activePlayers.Add(pd);
	}
}
