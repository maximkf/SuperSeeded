using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTexture : MonoBehaviour {

	public Texture[] textures;
	public Color[] colors;
	public GameObject background;
	public float waitTime;

	private Renderer rend, bgRend;
	private Color originalColor;
	private int color1, color2;
	private IEnumerator randomPattern;

	List<int> numbers = new List<int>();

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		bgRend = background.GetComponent<Renderer>();
		originalColor = bgRend.material.color;
		randomPattern = RandomPattern();
	}

	public void toggleRandom(bool toggle)
	{
		if(toggle){
			rend.enabled = true;
			StartCoroutine(randomPattern);
		}else{
			rend.enabled = false;
			StopCoroutine(randomPattern);
			bgRend.material.color = originalColor;
		}
	}

	IEnumerator RandomPattern(){
		while(true){
			getRandomUnique();
			float textureScale = Random.Range(1.5f, 4);
			rend.material.mainTexture = textures[Random.Range(0, textures.Length)];
			rend.material.mainTextureScale = new Vector2(textureScale, textureScale);
			rend.material.color = colors[color1];
			bgRend.material.color = colors[color2];
			yield return new WaitForSeconds(waitTime);
		}
		yield return null;
	}

	void getRandomUnique(){
		numbers.Clear();
		for (int i = 0; i < colors.Length; i++)
		{
			numbers.Add(i);
		}
		color1 = numbers[Random.Range(0, numbers.Count)];
		numbers.RemoveAt(color1);
		color2 = numbers[Random.Range(0, numbers.Count)];
	}
}
