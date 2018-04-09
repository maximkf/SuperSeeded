using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{

	private static T instance;

	public static T Instance
	{

		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<T>();
				if(instance == null)
				{
					var obj = new GameObject();
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
		set
		{
			instance = value;
		}
	}

	private void Awake(){
		DontDestroyOnLoad(this);
		if(instance == null)
		{
			instance = this as T;
		}else if(instance != null){
			Destroy(gameObject);
		}
	}

}
