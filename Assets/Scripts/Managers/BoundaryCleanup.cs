﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCleanup : MonoBehaviour {

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
