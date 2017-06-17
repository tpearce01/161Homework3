using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class BoundaryCleanup
 * 
 * Function: Clean up all game objects which have left the acceptable game area.
 *      Bullets are recycled into the object pool while all other objects
 *      are destroyed
 *********************************************************************************/
public class BoundaryCleanup : MonoBehaviour {

    //When an object collides with the boundary collider, if it is a projectile, recycle it
    //otherwise destroy it
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Background") )
        {
			if (other.gameObject.CompareTag ("Projectile")) {
				other.gameObject.SetActive (false);
			} else {
				Destroy (other.gameObject);
			}
        }
    }

    //When a trigger object collides with the boundary collider, if it is a projectile, recycle it
    //otherwise destroy it
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Background"))
        {
			if (other.gameObject.CompareTag ("Projectile")) {
				other.gameObject.SetActive (false);
			} else {
				Destroy (other.gameObject);
			}
        }
    }
}
