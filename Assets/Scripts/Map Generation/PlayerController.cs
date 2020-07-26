﻿using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float speed;
	
	//public Text countText; // Store a referenece to the UI Text component which will display the number of pickups collected.
	//public Text winText; 

	private int count; // integer to store the number of pickups collected so far

	private AudioSource source;

	private Rigidbody2D rigidbody;
	
	// Use this for initialization
	void Start()
	{
		count = 0;
		rigidbody = GetComponent<Rigidbody2D>();
		source = GetComponent<AudioSource>();
		// winText.gameObject.SetActive(false);
		
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code or control code here.
	void FixedUpdate()
	{
		// Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		// Store the current vertical input in the float moveVertical
		float moveVertical = Input.GetAxis ("Vertical");

		// Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		// Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rigidbody.AddForce (movement * speed);
	}

   void OnTriggerEnter2D(Collider2D other)
   {

	}


	// This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	// void SetCountText()
	// {
	// 	countText.text = "Count: " + count.ToString ();

	// 	if (count > 5)
	// 	{
	// 		// winText.gameObject.SetActive(true);
	// 		winText.enabled = true;
	// 	}
	// }
	
}
