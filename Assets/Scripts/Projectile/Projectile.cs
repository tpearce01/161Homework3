using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class Projectile
 * 
 * Function: Determines behavior of projectile obejcts. Default functionality is
 * to destroy after a timer. Parent of EnemyProjectile and DefaultProjectile
 *********************************************************************************/
public abstract class Projectile : MonoBehaviour {

    public int damage;      //Projectile damage
    public int speed;       //Travel speed of the projectile
    public float timeToLive;//Time left before the projectile is destroyed

    // Move the projectile
    void Update () {
		Move();
	    //CheckDestroy();
	}

    /// <summary>
    /// Move the projectile
    /// </summary>
    protected abstract void Move();

    /// <summary>
    /// Check if bullet should be destroyed. This functionality should be handled by
    /// cleanup boundaries. DEPRECATED FUNCTION
    /// </summary>
    protected void CheckDestroy()
    {
        if (timeToLive > 0)
        {
            timeToLive -= Time.deltaTime;
        }
        else
        {
			timeToLive = 2;
			DestroyProjectile();
        }
    }

    /// <summary>
    /// Return the projectile to the object pool
    /// </summary>
	public void DestroyProjectile(){
		gameObject.SetActive (false);
	}
}
