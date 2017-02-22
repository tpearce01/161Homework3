using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : MonoBehaviour
{
    public int damage;      //Projectile damage
    public int speed;       //Travel speed of the projectile
    public float timeToLive;//Time left before the projectile is destroyed
	
	void Update ()
	{
	    Move();
        CheckDestroy();
	}

    //Move bullet
    void Move()
    {
        gameObject.transform.position += transform.right;
    }

    //Check if bullet should be destroyed
    void CheckDestroy()
    {
        if (timeToLive > 0)
        {
            timeToLive -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Damage enemies on collision
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Unit>().ModifyHealth(-damage);
            Destroy(gameObject);
        }
    }
}
