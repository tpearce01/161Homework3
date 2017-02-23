using System.Collections;
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
			//SoundManager.i.PlaySound(Sound.PlayerHit, 0.5f);
			//Spawner.i.SpawnObject(Prefab.Hit0, 0.5f);
            other.gameObject.GetComponent<Unit>().ModifyHealth(-damage);
            Destroy(gameObject);
        }
    }
}
