using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedByContact : MonoBehaviour
{

	public GameObject explosion;
	public GameObject playerExplosion;

	public AudioSource aSource;
	public int scoreValue;
	private GameController gk;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gk = gameControllerObject.GetComponent<GameController> ();
		}
		if (gk == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

	}

	public void OnTriggerEnter (Collider other)
	{
		//Debug.Log(name);
		//Debug.Log(other.name);

		if (other.tag == "Boundry") {
			return;
		}

		if (other.tag == "Asteroid") {
			return;
		}

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {

			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gk.GameOver ();
		} else {

			gk.AddScore (scoreValue);
		}

		aSource.Play ();
     

		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
