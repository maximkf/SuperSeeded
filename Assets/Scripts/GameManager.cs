using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPersistent<GameManager> {

	public enum GameState {Title, PlayerSelect, GameReady, GameStart, GameEnd};
	public GameState currentGameState;
	public int playersConnected, endWaitTime;
	public float idleTimer;
	public bool hasInput;
	public GameObject[] playersToSpawn = new GameObject [2];
	public SpawnPoint[] spawnPoints = new SpawnPoint [2];
	public List <PlayerData> activePlayers = new List<PlayerData>();
	public RandomTexture patternShuffle;

	private bool playersSpawned, endScreen;
	private int winnerNum;
	private float idleTime;
	private GameObject winningPlayer;


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
				// GameStart();
			break;
			case GameState.GameEnd:
				GameEnd();
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
		checkIdle();
		endScreen = false;
		if(UIManager.Instance.fadeOver){
			LoadScene.load("Title");
			currentGameState = GameState.Title;
		}

		if(UIManager.Instance.totalSelected > 1){
			LoadScene.load("Game");
			currentGameState = GameState.GameReady;
		}
	}

	void GameReady(){
		endScreen = false;
		if(!playersSpawned){
			UIManager.Instance.countDown(0);
			SpawnPlayers();
			playersSpawned = true;
		}

		if(UIManager.Instance.countDownOver){
			patternShuffle = GameObject.Find("Pattern").GetComponent<RandomTexture>();
			currentGameState = GameState.GameStart;
		}
	}

	void GameRunning(){
		checkIdle();

		if(UIManager.Instance.fadeOver)
			LoadScene.load("PlayerSelect");
	}

	void GameEnd(){
		if(endScreen && UIManager.Instance.countDownOver){
			LoadScene.load("PlayerSelect");
			playersSpawned = false;
			currentGameState = GameState.PlayerSelect;
		}
	}

	IEnumerator EndSequence(){
		yield return new WaitForSeconds(1);
			// audioManager.playWinSequence(true);
			patternShuffle.toggleRandom(true);
		yield return new WaitForSeconds(1);
			patternShuffle.toggleRandom(false);
			// audioManager.playWinSequence(false);
			// activePlayers[winnerNum].isDead();
		yield return new WaitForSeconds(1);
			UIManager.Instance.displayWin(activePlayers[winnerNum].gameObject, endWaitTime);
			endScreen = true;
		yield break;
	}

	public void findWinningPlayer(int loserNum){
		foreach(PlayerData pd in activePlayers){
			if(pd.playerNum != loserNum){
				winnerNum = pd.playerNum;
			}
		}
		IEnumerator gameEnd = EndSequence();
		StartCoroutine(gameEnd);
		currentGameState = GameState.GameEnd;
	}

	void SpawnPlayers(){
		activePlayers.Clear();
		for(int i = 0; i < spawnPoints.Length; i++){
			if(spawnPoints[i] == null){
				GameObject sp = GameObject.Find("SpawnPoint" + i.ToString());
				spawnPoints[i] = sp.GetComponent<SpawnPoint>();
			}
			spawnPoints[i].spawnObject(playersToSpawn[i]);
			activePlayers[i].playerNum = i;
		}
		playersSpawned = true;
	}

	public void rematch(){
		Destroy(activePlayers[winnerNum].gameObject);
		playersSpawned = false;
		currentGameState = GameState.GameReady;
		// UIManager.Instance.countDown(3);
	}

	void checkIdle(){
			if(hasInput)
				idleTime = 0;

			idleTime += Time.deltaTime;
			if(idleTime > idleTimer){
				idleTime = 0;
				UIManager.Instance.timeOut(0);
			}
	}
}
