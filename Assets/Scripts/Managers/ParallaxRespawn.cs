using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class ParallaxRespawn
 * 
 * Function: Recycles
 *********************************************************************************/
public class ParallaxRespawn : MonoBehaviour {

    //Sets the cleanup barrier at the best position, based on source image width
	void Start(){
		gameObject.transform.position -= transform.right * Parallax.i.spriteWidth * 1.5f;
	}

    //Destroy all unexpected objects
	void OnCollisionEnter2D(Collision2D other)
	{
	    Destroy(other.gameObject);
	}

    //Reset background position when a collision is detected
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Background")){
			other.transform.position += Vector3.right * Parallax.i.spriteWidth * 2;
        }
	}
}
