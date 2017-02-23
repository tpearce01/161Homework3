﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{

    //Move bullet
    protected override void Move()
    {
        gameObject.transform.position -= transform.right;
    }

    //Damage enemies on collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Unit>().ModifyHealth(-damage);
            Destroy(gameObject);
        }
    }
}