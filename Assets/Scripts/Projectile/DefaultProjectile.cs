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
		if (other.gameObject.CompareTag("Enemy") )
        {
            SoundManager.i.PlaySound(Sound.Hit, SoundManager.i.volume / 4);
            Spawner.i.SpawnObject(Prefab.Sparks, gameObject.transform.position);
            other.gameObject.GetComponent<Unit>().ModifyHealth(-damage);
			DestroyProjectile();
        }

        if ( other.gameObject.CompareTag("Turret"))
        {
            Spawner.i.SpawnObject(Prefab.Sparks, gameObject.transform.position);
            other.gameObject.GetComponent<Turret>().ModifyHealth(-damage);
            DestroyProjectile();
        }
    }
}
