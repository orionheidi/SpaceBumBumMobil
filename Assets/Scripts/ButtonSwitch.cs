using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
	public AudioSource myMusic;
	public GameObject play;
	public GameObject settings;
	public GameObject quit;
	public InputField textInput;
	public Player player;
	public Animator scaleButton;


	void GetPlayers (string Key)
	{
		List<string> players1 = new List<string> ();

		for (int i = 0; i < PlayerPrefs.GetInt (Key + "L", 0); i++) {
			players1.Add (PlayerPrefs.GetString (Key + i, ""));
			Debug.Log (PlayerPrefs.GetString (Key + i, ""));
		}
	}

	private void Awake ()
	{
		GameObject spash = GameObject.Find ("CustomSplashScreen");
		spash.GetComponent<SplashScreenScript> ().FadeOut ();
	}

	private void Start ()
	{
		scaleButton.Play ("scale");
		//PlayerPrefs.GetInt("NESTO", 0);
		Debug.Log (PlayerPrefs.GetString ("playerName", ""));

		GetPlayers ("IGRACI");
		GetPlayers ("SKOROVI");
	}

	public void goToMainScene (string scene)
	{

		PlayerPrefs.SetString ("playerName", textInput.text);

		GameObject spash = GameObject.Find ("CustomSplashScreen");
		spash.GetComponent<SplashScreenScript> ().FadeIn ();

		StartCoroutine (FadingScene (scene));
        


	}

	IEnumerator FadingScene (string scene)
	{

		yield return new WaitForSeconds (2f);

		//yield return new WaitUntil(() => blackImage.color.a == 1);
		SceneManager.LoadScene (scene);
	}

 
	public void GoToOptionScene (string scene)
	{

		SceneManager.LoadScene (scene);
	}

	public void QuitGame ()
	{
		Debug.Log ("Has quit a game!");
		Application.Quit ();
		myMusic.volume = 0;
	}



}
