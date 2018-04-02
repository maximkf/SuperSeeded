using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomVector {

	public static Vector3 getRandomUnitVector(){
		return new Vector3(
			Random.Range(-1, 1f),
			Random.Range(-1, 1f),
			Random.Range(-1, 1f)
			);
	}
}
