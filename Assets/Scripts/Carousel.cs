using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour {

	public GameObject[] objects;
	public string selectedObjectName;
	public float speed, radius, spinRate, offset;
	public int selectedObject;
	public bool spinActive;
	public Camera camera;

	private float angleIncrement, angle;
	private Vector3 startPoint;
	private Quaternion targetRotation;
	private int playerNumber;
	private Color objectColor;
	private List <GameObject> carouselObjects = new List<GameObject>();

	void Start () {
		angle = 360f/objects.Length;

		playerNumber = GetComponent<PlayerInput>().playerNum;

		startPoint = new Vector3(transform.position.x - radius, transform.position.y,
		transform.position.z);

		ArrangeObjects();
	}

	// Update is called once per frame
	void Update () {
		float step = speed * angle * Time.deltaTime;

		angleIncrement = angle * selectedObject - offset;
		targetRotation.eulerAngles = new Vector3 (0, angleIncrement, 0);

		camera.transform.rotation = targetRotation;

		if(spinActive)
			Spin(selectedObject);

	}

	void ArrangeObjects(){
		carouselObjects.Clear();

		//rotate by the angle * array position
		for(int i = 0; i < objects.Length; i++)
		{
			GameObject go = Instantiate(objects[i], startPoint, Quaternion.identity, this.transform);
			go.transform.RotateAround (transform.position, Vector3.up, angle * i);
			carouselObjects.Add(go);
		}
		pickRandomStart((int)Random.Range(0,objects.Length));
		print(carouselObjects.Count);
	}

	void pickRandomStart(int num){
		selectedObject = num;

		camera.transform.rotation = targetRotation;
		
		selectedObjectName = carouselObjects[selectedObject].name;
		UIManager.Instance.setBackgroundColor(playerNumber, objects[selectedObject]);
	}

	void Spin(int num){
		// foreach(GameObject g in carouselObjects){
		// 	Quaternion rot = g.transform.rotation;
		// 	rot.eulerAngles += new Vector3 (0, spinRate, 0);
		// 	g.transform.rotation = rot;
		// }
		for(int i = 0; i < carouselObjects.Count; i++){
			if(i == selectedObject){
				Quaternion rot = carouselObjects[i].transform.rotation;
				rot.eulerAngles += new Vector3 (0, spinRate, 0);
				carouselObjects[i].transform.rotation = rot;
			}
		}
	}

	public void rotate(string direction){
		switch(direction)
		{
			case "Left":
				selectedObject ++;
			break;
			case "Right":
				selectedObject --;
			break;
			default:
			break;
		}

		if(selectedObject < 0)
			selectedObject = objects.Length-1;
		else if(selectedObject > objects.Length-1)
			selectedObject = 0;
		selectedObjectName = carouselObjects[selectedObject].name;
		UIManager.Instance.setBackgroundColor(playerNumber, objects[selectedObject]);
	}

}
