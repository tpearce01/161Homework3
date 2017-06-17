using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class EnemyProjectile : Projectile
 * 
 * Function: Extend projectile functionality to move left and damage players
 *********************************************************************************/
public class EnemyProjectile : Projectile
{

    /// <summary>
    /// Move the bullet to the left
    /// </summary>
    protected override void Move()
    {
		gameObject.transform.position -= transform.right * speed * Time.deltaTime;
    }

    //Damage enemies on collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			SoundManager.i.PlaySound(Sound.Hit, SoundManager.i.volume / 4);
			Spawner.i.SpawnObject(Prefab.Sparks, gameObject.transform.position);
            other.gameObject.GetComponent<Unit>().ModifyHealth(-damage);
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                other.gameObject.GetComponent<PlayerController>().DamageVisual();
            }
            else
            {
                other.gameObject.GetComponent<FusedPlayer>().DamageVisual();
            }
            DestroyProjectile();
        }
    }
}
