using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class DefaultProjectile : Projectile
 * 
 * Function: Controls the functionality of a player-spawned projectile. Moves 
 *      right and damages enemies
 *********************************************************************************/
public class DefaultProjectile : Projectile
{
	public int player;      //Player who spawned this projectile

    /// <summary>
    /// Move the bullet to the right
    /// </summary>
    protected override void Move()
    {
		gameObject.transform.position += transform.right * speed * Time.deltaTime;
    }

    //Damage enemies on collision
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.CompareTag("Enemy") )
        {
            SoundManager.i.PlaySound(Sound.Hit, SoundManager.i.volume / 4);
            Spawner.i.SpawnObject(Prefab.Sparks, gameObject.transform.position);
            other.gameObject.GetComponent<Unit>().ModifyHealth(-damage);
            if(other.gameObject.GetComponent<Unit>().health < 0)
            {
                Level.i.updateScore(25);
                HUDManager.i.UpdateScore();
            }

			DestroyProjectile();
        }
    }
}
