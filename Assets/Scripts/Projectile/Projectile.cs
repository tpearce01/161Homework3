using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    public int damage;      //Projectile damage
    public int speed;       //Travel speed of the projectile
    public float timeToLive;//Time left before the projectile is destroyed

    // Update is called once per frame
    void Update () {
		Move();
	    //CheckDestroy();
	}

    protected abstract void Move();

    //Check if bullet should be destroyed
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

	public void DestroyProjectile(){
		gameObject.SetActive (false);
	}
}
