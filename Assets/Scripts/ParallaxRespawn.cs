using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxRespawn : MonoBehaviour {

	void Start(){
		gameObject.transform.position -= transform.right * Parallax.i.spriteWidth;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
	    Destroy(other.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Trigger: " + other.gameObject.name);
		if(other.gameObject.CompareTag("Background")){
			other.transform.position += Vector3.right * Parallax.i.spriteWidth * 2;
        }
	}
}
