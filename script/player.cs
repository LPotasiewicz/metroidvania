using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public Rigidbody2D rb2d;
	public Animator anim;
	public float speed;
	public float jumpHeight;
	public bool grounded;
	public GameObject attackObject;
	public GameObject deathObject;
	public GameObject attackSpawn;
	public int health;

	private Vector2 movement;
	private int attackInstanceID = -1;
	private bool isRunning;
	public bool isHit;
	private bool canAttack = true;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}

	// Take Damage
	void OnTriggerStay2D (Collider2D other){
		if (other.gameObject.tag == "enemy") {
			if (other.gameObject.GetComponent<attack> ().GetInstanceID () != attackInstanceID) {
				attackInstanceID = other.gameObject.GetComponent<attack> ().GetInstanceID ();
				health -= other.gameObject.GetComponent<attack> ().value;
				Debug.Log ("ow " + health);
				StartCoroutine (hitDelay (other.gameObject));
			}
		}
	}

	
	// Update is called once per frame
	void Update () {

		//Trigger on Death
		if (health <= 0) {
			GameObject deathAnim = Instantiate (deathObject, gameObject.transform.position, Quaternion.identity) as GameObject;
			Destroy (deathAnim, 1f);
			Destroy (gameObject);
		}
		movement = new Vector2 (rb2d.velocity.x, rb2d.velocity.y);
		Vector2 direction = new Vector2 (gameObject.transform.localScale.x, 1);


		isRunning = false;
		// WALKING
		if (!isHit) {
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

			// ATTACKING
			if (Input.GetKeyDown (KeyCode.Space) && canAttack) {
				GameObject attack = Instantiate (attackObject, attackSpawn.transform.position, Quaternion.identity) as GameObject;
				Destroy (attack, 0.1f);
				StartCoroutine (attackDelay ());
			}
		}

		// ASSIGN INGAME VARIABLES
		rb2d.velocity = movement;
		gameObject.transform.localScale = direction;

		// ANIMATION VARABLES
		anim.SetBool ("isRunning", isRunning);
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("yVelocity", movement.y);

	
	}

	IEnumerator attackDelay(){
		canAttack = false;
		yield return new WaitForSeconds(0.5f);
		canAttack = true;
	}

	IEnumerator hitDelay(GameObject enemy){
		int direction = (enemy.transform.position.x > gameObject.transform.position.x) ? -1 : 1;
		rb2d.velocity = new Vector2(direction * 3, 3);
		isHit = true;
		yield return new WaitForSeconds(0.3f);
		isHit = false;
		yield return new WaitForSeconds(0.3f);
		attackInstanceID = -1;
	}
}
