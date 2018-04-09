using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMeshTri : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Camera c = Camera.main;
		Vector3[] vertices = new Vector3[]
		{
			c.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10)),
			c.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)),
			c.ScreenToWorldPoint(new Vector3(0, 0, 10)),
		};

		int[] triangles = new int[]
		{
			0,1,2
		};

		mesh.Clear();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
	}

	// Update is called once per frame
	void Update () {

	}
}
