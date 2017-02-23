using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0Behavior : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float timeBetweenAttacks;
    public float attackCooldown;
    public int speed;
    public GameObject attackType;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate ()
	{
	    Move();				
	}

	void Update(){
		//Shoot ();
		ShootDirected ();
	}

	//Move straight toward the left side of the screen
    void Move()
    {
        rigidbody.transform.position -= transform.right*speed*Time.deltaTime;
    }

	//Basic shot, moves left only
	void Shoot()
	{
		if (attackCooldown > 0)
		{
			attackCooldown -= Time.deltaTime;
		}
		else
		{
			attackCooldown = timeBetweenAttacks;
			//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
			Spawner.i.SpawnObject (Prefab.Shot2, gameObject.transform.position);
		}
	}

	//Simple shot, shot toward a random player
	void ShootDirected(){
		if (attackCooldown > 0)
		{
			attackCooldown -= Time.deltaTime;
		}
		else
		{
			attackCooldown = timeBetweenAttacks;
			//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
			Spawner.i.SpawnObjectTowardRandomPlayer (Prefab.Shot2, gameObject.transform.position);
		}
	}

	//If the unit collides with the player, destroy it
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("Player")){
			other.gameObject.GetComponent<Unit>().ModifyHealth(-20);
			//SoundManager.i.PlaySound (Prefab.Explosion1, 0.5f);
			//Spawner.i.SpawnObject (Prefab.Explosion1, gameObject.transform.position);
			Destroy(gameObject);
		}
	}
}
