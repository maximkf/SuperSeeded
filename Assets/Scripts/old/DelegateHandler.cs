using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateHandler : MonoBehaviour {

	private static DelegateHandler _instance;
	public static DelegateHandler Instance
	{
		get
		{
			if(_instance == null)
			{
				GameObject go = new GameObject("GameManager");
				go.AddComponent<GameManager>();
			}
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
	}

	public delegate void onRingOut(string camera);

	public static event onRingOut setActiveCamera;


}
