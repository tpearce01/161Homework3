using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : Projectile
{
    //Move bullet
    protected override void Move()
    {
		gameObject.transform.position += transform.right * speed * Time.deltaTime;
    }

    //Damage enemies on collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //SoundManager.i.PlaySound(Sound.EnemyHit, 0.5f);
            //Spawner.i.SpawnObject(Prefab.Hit0, 0.5f);
            other.gameObject.GetComponent<Unit>().ModifyHealth(-damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Turret"))
        {
            //SoundManager.i.PlaySound(Sound.EnemyHit, 0.5f);
            //Spawner.i.SpawnObject(Prefab.Hit0, 0.5f);
            other.gameObject.GetComponent<Turret>().ModifyHealth(-damage);
            Destroy(gameObject);
        }

    }
}
