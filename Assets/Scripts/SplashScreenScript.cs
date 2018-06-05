using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenScript : MonoBehaviour
{

	public static SplashScreenScript instance = null;

	Animator _anim;

	private void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		_anim = GetComponent<Animator> ();
	}

	public void FadeIn ()
	{
		Debug.Log ("ZOVEM");
		this.transform.GetChild (0).gameObject.SetActive (true);
		_anim.Play ("SplashFadeIn");
	}

	public void FadeOut ()
	{
		Debug.Log ("ZOVEM FadeOut");
		_anim.Play ("-SplashFadeIn");
		StartCoroutine (CLoseOBject ());
	}

	IEnumerator CLoseOBject ()
	{
		yield return new WaitForSeconds (2f);
		this.transform.GetChild (0).gameObject.SetActive (false);
	}
}
