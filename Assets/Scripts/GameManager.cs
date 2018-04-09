using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPersistent<GameManager> {

	public enum GameState {Title, PlayerSelect, GameReady, GameStart, GameEnd};
	public GameState currentGameState;
	public int playersConnected;
	public GameObject[] playersToSpawn = new GameObject [2];
	public SpawnPoint[] spawnPoints = new SpawnPoint [2];
	public List <GameObject> activePlayers = new List<GameObject>();

	private bool playersSpawned;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		switch(currentGameState){
			case GameState.Title:
				Title();
			break;
			case GameState.PlayerSelect:
				PlayerSelect();
			break;
			case GameState.GameReady:
				GameReady();
			break;
			case GameState.GameStart:
			break;
			default:
			break;
		}

		if(playersConnected < 0)
			playersConnected = 0;
	}

	void Title(){
		if(Input.anyKeyDown){
			LoadScene.load("PlayerSelect");
			currentGameState = GameState.PlayerSelect;
		}
	}

	void PlayerSelect(){
		if(UIManager.Instance.totalSelected > 1){
			LoadScene.load("Game");
			currentGameState = GameState.GameReady;
		}
	}

	void GameReady(){
		if(!playersSpawned){
			UIManager.Instance.countDown(0);
			playersSpawned = true;
		}

		if(UIManager.Instance.countDownOver){
			currentGameState = GameState.GameStart;
			Invoke("SpawnPlayers", 0);
		}
	}

	void SpawnPlayers(){
		activePlayers.Clear();
		for(int i = 0; i < spawnPoints.Length; i++){
			if(spawnPoints[i] == null){
				GameObject sp = GameObject.Find("SpawnPoint" + i.ToString());
				spawnPoints[i] = sp.GetComponent<SpawnPoint>();
			}
			spawnPoints[i].spawnObject(playersToSpawn[i]);
			activePlayers[i].GetComponent<PlayerData>().playerNum = i;
		}
	}
}
