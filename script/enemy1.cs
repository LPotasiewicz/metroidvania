using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : MonoBehaviour {
	
	public int health;
	public GameObject player;
	public Rigidbody2D rb2d;
	public Animator anim;
	public float speed;

	private Vector2 movement;
	private bool isRunning = true;
	private bool isHit = false;
	private int attackInstanceID = -1;

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "attack" ) {
			if (other.gameObject.GetComponent<attack> ().GetInstanceID() != attackInstanceID) {
				attackInstanceID = other.gameObject.GetComponent<attack> ().GetInstanceID();
				health -= other.gameObject.GetComponent<attack> ().value;
				Debug.Log ("ow " + health);
				StartCoroutine (hitDelay ());
			}
		}
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (gameObject);
		}

		movement = new Vector2 (rb2d.velocity.x, rb2d.velocity.y);
		Vector2 direction = new Vector2 (gameObject.transform.localScale.x, 1);

		// WALKING
		isRunning = false;
		if (Vector2.Distance (gameObject.transform.position, player.transform.position) < 5 && !isHit) {
			if (player.transform.position.x > gameObject.transform.position.x) {
				movement.x = speed;
				direction.x = 1;
				isRunning = true;
			} else {
				movement.x = -speed;
				direction.x = -1;
				isRunning = true;
			}
		}

		// ASSIGN INGAME VARIABLES
		rb2d.velocity = movement;
		gameObject.transform.localScale = direction;

		// ANIMATION VARABLES
		anim.SetBool ("isRunning", isRunning);
	}
	IEnumerator hitDelay(){
		int direction = (player.transform.position.x > gameObject.transform.position.x) ? -1 : 1;
		rb2d.velocity = new Vector2(direction * 3, 3);
		isHit = true;
		yield return new WaitForSeconds(0.5f);
		isHit = false;
	}
}
