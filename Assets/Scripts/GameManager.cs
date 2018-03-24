using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


	private static GameManager _instance;
	public static GameManager Instance
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

	public enum GameState {Load, Start, End, Reset};

	public bool toggleCam, singlePlayer;
	public float idleTimer;
	public Transform[] spawnPosition;
	public Player[] playerPrefabs;
	public List <Player> players = new List<Player>();
	public int playersConnected {get; set;}
	public GameObject groundPlane, bumpReadyIcon, burstPrefab;
	public Bump bumpPrefab;
	public UIManager canvas;
	public RandomTexture patternShuffle;

	[SerializeField]
	private int playerTotal, winnerNum, loserNum;
	private float idleTime;
	private bool camBit, title;
	private IEnumerator gameEnd;
	private GameObject CameraB;
	private RingOut ring;
	private GameState currentGameState;
	private AudioManager audioManager;
	private CollisionManager collisionManager;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		_instance = this;
	}

	void Start () {
		canvas = GetComponent<UIManager>();
		audioManager = GetComponent<AudioManager>();
		collisionManager = GetComponent<CollisionManager>();
		// CameraB = GameObject.Find("/CameraB");
		gameEnd = GameEnd();
		currentGameState = GameState.Load;
	}

	void Update () {
		// canvas.updateDeviceText(playersConnected == playerTotal);

		switch(currentGameState)
		{
			case GameState.Load :
				currentGameState = GameState.Start;
				title = (SceneManager.GetActiveScene().name == "Title") ? true : false;
				spawnPlayers();
				setupPlayingField();
				ring.resetRing(players[0], players[1]);
				break;
			case GameState.Start :
				ring.lerpRingColor();
				canvas.fadeDirections(hasMovement());
				if(singlePlayer)
					autoTarget();
				if(title)
					canvas.updateDirections(playersConnected);
				break;
			case GameState.Reset:
				currentGameState = GameState.Start;
				spawnPlayers();
				// ring.setPlayers();
				ring.resetRing(players[0], players[1]);
				break;
			default:
				break;
		}

		if(playersConnected < 0)
			playersConnected = 0;

		// if(title && Input.GetKeyDown("space") && currentGameState == GameState.Start){
		// 	LoadScene.load("Game");
		// 	currentGameState = GameState.Load;
		// 	canvas.reset();
		// }

		// if(TimedOut()){
		// 	idleTime = 0;
		// 	LoadScene.load("Title");
		// 	currentGameState = GameState.Load;
		// 	canvas.reset();
		// }
	}

	void spawnPlayers(){
		players.Clear();

		for(int i = 0; i <= playerTotal-1; i++){
			int j = Random.Range(0, playerPrefabs.Length);//pick a random fruit
			Player player = Instantiate(playerPrefabs[j], spawnPosition[i].position, Random.rotation) as Player;//instance player
			player.setupPlayer(i, title);//run player setup
			players.Add(player);//add to players array
		}
	}

	void setupPlayingField(){
		GameObject playingField = GameObject.Find("/Playing Field");
		groundPlane = GameObject.Find("/Playing Field/Ground Plane");
		collisionManager.groundPlane = groundPlane;
		ring = playingField.GetComponentInChildren<RingOut>();
		patternShuffle = playingField.GetComponentInChildren<RandomTexture>();
	}

	void autoTarget(){
		Move move = players[1].gameObject.GetComponent<Move>();
		if(players[0] != null){
			move.autoTarget = true;
			move.target = players[0].gameObject;
		}else{
			move.autoTarget = false;
			move.target = players[1].gameObject;
		}
	}

	public bool hasMovement(){
		return (players[0].moving || players[1].moving)? true: false;
	}

	void toggleCameras(){
		camBit = !camBit;
		CameraB.GetComponent<FollowObject>().objectToFollow = players[winnerNum].gameObject;
		Camera.main.enabled = !camBit;
		CameraB.GetComponent<Camera>().enabled = camBit;
	}

	public void onGameEnd(Player losingPlayer){
		// TODO: update UIManager/score?
		currentGameState = GameState.End;
		losingPlayer.lost = true;
		loserNum = losingPlayer.playerNum;
		findWinningPlayer();
		gameEnd = GameEnd();
		StartCoroutine(gameEnd);
	}

	IEnumerator GameEnd(){
			// currentGameState = GameState.End;
		yield return new WaitForSeconds(1);
			if(toggleCam)
				toggleCameras();
			audioManager.playWinSequence(true);
			patternShuffle.toggleRandom(title, true);
		yield return new WaitForSeconds(1);
			patternShuffle.toggleRandom(title, false);
			audioManager.playWinSequence(false);
			players[winnerNum].isDead();
		yield return new WaitForSeconds(1);
			resetGame();
		yield return null;
	}

	void findWinningPlayer(){
		foreach(Player p in players){
			if(p.playerNum != loserNum)
				winnerNum = p.playerNum;
		}
	}

	public void resetGame(){
		currentGameState = GameState.Reset;
		StopCoroutine(gameEnd);
	}

	bool TimedOut(){
		foreach(Player p in players){
			if(p.moving)
				idleTime = 0;
		}
		idleTime += Time.deltaTime;
		print(idleTime + "Timed Out");
		return (!title && idleTime > idleTimer)? true : false;
	}

	// void OnGUI(){
	// 	GUI.Box(new Rect (10,10,100,30),new GUIContent(currentGameState.ToString()));
	// }

}
