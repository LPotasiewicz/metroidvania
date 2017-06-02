using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDumbass : MonoBehaviour {

	public int health;

	void OnTriggerStay2D (Collider2D other){
		if (other.gameObject.tag == "attack") {
			health-= other.gameObject.GetComponent<attack> ().value;
			Debug.Log ("Ow" + health);
			Destroy (other.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
