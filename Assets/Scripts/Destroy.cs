﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public void destroySelf(){
		Destroy(this.gameObject);
	}
}
