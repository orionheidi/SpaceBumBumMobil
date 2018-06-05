
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameController : MonoBehaviour
{

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float weaveWait;

	public Text scoreText;
	private int score;
	public Text restartText;
	public Text gameOverText;
 

	private bool gameOver;
	private bool restart;
	public Mover mover;
	public float newScore;

	public bool pause;
	public bool quit;
	public AudioSource music;
	// public Animator fadingOut;
	public Animator fadingIn;

	public Slider slider;
	public Text highestScoreController;
	public Text newHighest;
	private PlayerProgress playerProgress;
	private Player player;
	int[] players = new int[5];

	public float waitScore;
	public GameObject panelPlayer;
	public Text playerDisplayText;
	int playerSize;
  

	public CanvasGroup panel;

	private void Awake ()
	{

		GameObject spash = GameObject.Find ("CustomSplashScreen");
		spash.GetComponent<SplashScreenScript> ().FadeOut ();
	}

	void Start ()
	{
      
		gameOver = false;
		restart = false;
		pause = false;
		quit = false;
		newHighest.text = "";
		restartText.text = "";
		gameOverText.text = "";
     
		panel.gameObject.SetActive (false);
		score = 0; 
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		LoadPlayerProgress ();
		highestScoreController.gameObject.SetActive (true);
		SubmitPlayerScore (score);
		LoadPlayerIndividualProgress ();
		LoadPlayerName ();
      
		highestScoreController.text = "Highest Score: " + GetHighestPlayerScore ().ToString ();
      
		//PlayerPrefs.DeleteAll();
		



	}

	private void Update ()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene ("Main");
			}
		}

		ScoreProgress ();

		music.volume = slider.value;

		// PauseGame();
	}


	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (UnityEngine.Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

				//Debug.Log(spawnPosition);

				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (weaveWait);


			if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}

		}
	}

	IEnumerator WaitDisplayHighestScore ()
	{
      
           
		newHighest.gameObject.SetActive (true);

		newHighest.text = "YOUR HIGHEST SCORE " + score.ToString ();

		yield return new WaitForSeconds (waitScore);

		//StopCoroutine(WaitDisplayHighestScore());

		newHighest.gameObject.SetActive (false);



	}

	bool hgh = false;

	public void  AddScore (int newScore)
	{

		if (playerProgress.highestScore <= score) {
			score += newScore;
			if (!hgh) {
				hgh = true;
				StartCoroutine (WaitDisplayHighestScore ());
			}
            

			highestScoreController.text = "Highest Score: " + score.ToString ();
			;

        
		} else {


			score += newScore;

			newHighest.gameObject.SetActive (false);
		}
		UpdateScore ();

	}



	public IEnumerator FadeCanvasGroup (CanvasGroup cg, float start, float end, float leprTime)
	{
		float timeStartedLerping = Time.time;
		float timeSinceStarted = Time.time - timeStartedLerping;
		float procentageComplete = timeSinceStarted / leprTime;

		while (true) {

			timeSinceStarted = Time.time - timeStartedLerping;
			procentageComplete = timeSinceStarted / leprTime;

			float currentValue = Mathf.Lerp (start, end, procentageComplete);

			cg.alpha = currentValue;

			if (procentageComplete >= 1)
				break;

			yield return new WaitForEndOfFrame ();
			Debug.Log ("Done");
		}
      
	}

	public void UpdateScore ()
	{     
		scoreText.text = "Score: " + score;

	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		highestScoreController.gameObject.SetActive (true);
		SubmitPlayerScore (score);
		highestScoreController.text = "Highest Score: " + GetHighestPlayerScore ().ToString ();
		panelPlayer.gameObject.SetActive (true);
		SavePlayerName ();
		SavePlayerScore ();
		// playerDisplayText.text = "P: " + GetPlayerName() + " " + GetPlayerScore().ToString();
		ToConsole ();   
		playerSize = players.Length;
		Debug.Log ("Player size: " + playerSize);

       


	}

   
	public void ScoreProgress ()
	{


		if (score >= 50) {

			mover.speed = -6;

		} else if (score >= 100) {

			mover.speed = -8;
		} else if (score >= 150) {

			mover.speed = -15;
		}

	}

	public void onPause ()
	{
		pause = !pause;

		if (!pause) {
			Time.timeScale = 1;
           
			fadingIn.Play ("fadingOut");

		} else if (pause) {
			Time.timeScale = 0;

			panel.gameObject.SetActive (true);
			fadingIn.Play ("fadingIn");
            
         
		}
	}

	public void QuitGame ()
	{
		Debug.Log ("Has quit a game!");

		if (!quit) {
			Application.Quit ();
			Time.timeScale = 0;
			music.volume = 0;
			panel.gameObject.SetActive (false);

		}
	}



	public void LoadPlayerProgress ()
	{
		playerProgress = new PlayerProgress ();

		if (PlayerPrefs.HasKey ("highestScore")) {
			playerProgress.highestScore = PlayerPrefs.GetInt ("highestScore");
		}

	}

   

	public void SubmitPlayerScore (int newScore)
	{
		if (newScore > playerProgress.highestScore) {
			playerProgress.highestScore = newScore;
			SavePlayerProgress ();
		}

	}

	public int GetHighestPlayerScore ()
	{
		return playerProgress.highestScore;
	}



	public void SavePlayerProgress ()
	{
		PlayerPrefs.SetInt ("highestScore", playerProgress.highestScore);
	}



	public void LoadPlayerIndividualProgress ()
	{
		player = new Player ();

		if (PlayerPrefs.HasKey ("playerScore")) {
			player.IndividualScore = PlayerPrefs.GetInt ("playerScore");
		}

	}


	public void LoadPlayerName ()
	{
		player = new Player ();

		if (PlayerPrefs.HasKey ("playerName")) {
			player.PlayerName = PlayerPrefs.GetString ("playerName");
		}

	}


	public void SavePlayerScore ()
	{
       

		player.IndividualScore = player.IndividualScore + score;
		PlayerPrefs.SetInt ("playerScore", player.IndividualScore);
		Debug.Log ("<color=blue>" + player.IndividualScore + "</color>");

		List<string> tmp = GetPlayers ("SKOROVI");
		tmp.Add (player.IndividualScore.ToString ());
		Debug.Log (tmp.Count);
		SetPlayers ("SKOROVI", tmp);


	}

	public void SavePlayerName ()
	{
		Debug.Log ("<color=red>" + player.PlayerName + "</color>");
		PlayerPrefs.SetString ("playerName", player.PlayerName);

		List<string> tmp = GetPlayers ("IGRACI");
		tmp.Add (player.PlayerName.ToString ());
		Debug.Log (tmp.Count);
		SetPlayers ("IGRACI", tmp);
	}

	public int GetPlayerScore ()
	{
		return player.IndividualScore;
	}

	public string  GetPlayerName ()
	{


		return  player.PlayerName;
	}

	void SetPlayers (string Key, List<string> players)
	{
		PlayerPrefs.SetInt (Key + "L", players.Count);
		for (int i = 0; i < players.Count; i++) {          
			PlayerPrefs.SetString (Key + i, players [i].ToString ());
		}
	}


	List<string> GetPlayers (string Key)
	{
		List<string> players1 = new List<string> ();

		for (int i = 0; i < PlayerPrefs.GetInt (Key + "L", 0); i++) {
			players1.Add (PlayerPrefs.GetString (Key + i, ""));  
		}

		return players1;
	}

	void ToConsole ()
	{

		List<string> Igraci = GetPlayers ("IGRACI");
		List<string> Skorovi = GetPlayers ("SKOROVI");

		string textNeki = "";

		for (int i = 0; i < Igraci.Count; i++) {
			textNeki = textNeki + "IGRAC: " + Igraci [i] + " SKOR: " + Skorovi [i] + "\n";
			Debug.Log ("IGRAC: " + Igraci [i] + " SKOR: " + Skorovi [i]);
            
			Debug.Log ("SORTIRANO - " + "  IGRAC: " + Igraci [i] + " SKOR: " + Skorovi [i]);

		}

		playerDisplayText.text = textNeki;
       
	}
}

/*  public void PauseGame()
      {
          if (Input.GetKeyDown(KeyCode.L))
          {

              if (Time.timeScale == 1.0F)
                  Time.timeScale = 0.0F;

              else
                  Time.timeScale = 1.0F;
          }
      }*/


