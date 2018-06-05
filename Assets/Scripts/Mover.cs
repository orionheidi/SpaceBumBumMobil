using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

	public float speed;

	public Rigidbody rb;

	private Rigidbody rb_private;


	public void Start ()
	{
		//rb.GetComponent<Rigidbody>();

		rb_private = GetComponent<Rigidbody> ();

		rb_private.velocity = transform.forward * speed;
        
	}
}
