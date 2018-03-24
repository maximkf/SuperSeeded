using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	public static void load (string scene) {
		SceneManager.LoadScene(scene);
	}

}
