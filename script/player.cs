using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public Rigidbody2D rb2d;
	public Animator anim;
	public float speed;
	public float jumpHeight;
	public bool grounded;

	private Vector2 movement;
	private bool isRunning;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		movement = new Vector2 (rb2d.velocity.x, rb2d.velocity.y);
		Vector2 direction = new Vector2 (gameObject.transform.localScale.x, 1);

		isRunning = false;
		// WALKING
		if (Input.GetKey (KeyCode.A)) {
			direction.x = -1;
			movement.x = -speed;
			isRunning = true;
		}
		if (Input.GetKey (KeyCode.D)) {
			direction.x = 1;
			movement.x = speed;
			isRunning = true;
		} 
		if (grounded && !isRunning) {
			movement.x = rb2d.velocity.x * 0.5f;
		}

		// JUMPING
		if (Input.GetKeyDown (KeyCode.W) && grounded) {
			movement.y = jumpHeight;
		}

		// ASSIGN INGAME VARIABLES
		rb2d.velocity = movement;
		gameObject.transform.localScale = direction;
	}

	void Update () {
		// ANIMATION VARABLES
		anim.SetBool ("isRunning", isRunning);
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("yVelocity", movement.y);
	}
}
