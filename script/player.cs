using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public Rigidbody2D rb2d;
	public float speed;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movement = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
		if (Input.GetKey (KeyCode.A)) {
			movement.x = -speed;
		} else if (Input.GetKey (KeyCode.D)) {
			movement.x = speed;
		}
		rb2d.velocity = new Vector2(movement.x, movement.y);
	}
}
