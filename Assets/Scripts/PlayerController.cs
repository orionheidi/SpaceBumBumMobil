using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundry
{
	public float xMin, xMax, zMin, zMax;
}



public class PlayerController : MonoBehaviour
{
    
	public Rigidbody rigidbody1;
	public Boundry boundry;

	public float speed;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;
	public AudioSource audio1;

	private Vector3 pozicjaMisa;
	private Vector3 pozicijaBroda;
	private Vector3 distanca;


	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audio1.Play ();
		}
   
	}

   

	void FixedUpdate ()
	{

		if (Input.GetMouseButtonDown (0)) {

			pozicjaMisa = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			pozicijaBroda = rigidbody1.position;

			distanca = pozicjaMisa - pozicijaBroda;
		}


		if (Input.GetMouseButton (0)) {
			pozicjaMisa = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			rigidbody1.position = new Vector3 (pozicjaMisa.x - distanca.x, 0.0f, pozicjaMisa.z - distanca.z);
       

			rigidbody1.position = new Vector3 (
				Mathf.Clamp (rigidbody1.position.x, boundry.xMin, boundry.xMax),
				0.0f,
				Mathf.Clamp (rigidbody1.position.z, boundry.zMin, boundry.zMax)
			);

			rigidbody1.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody1.velocity.x * -tilt);

			if (this.GetComponent<Rigidbody> ().position.x == boundry.xMin || this.GetComponent<Rigidbody> ().position.x == boundry.xMax) {

				pozicjaMisa = Camera.main.ScreenToWorldPoint (Input.mousePosition);

				pozicijaBroda = rigidbody1.position;

				distanca = pozicjaMisa - pozicijaBroda;
			}
			if (this.GetComponent<Rigidbody> ().position.z == boundry.zMin || this.GetComponent<Rigidbody> ().position.z == boundry.zMax) {

				pozicjaMisa = Camera.main.ScreenToWorldPoint (Input.mousePosition);

				pozicijaBroda = rigidbody1.position;

				distanca = pozicjaMisa - pozicijaBroda;
			}
		}
	}
		        
}

