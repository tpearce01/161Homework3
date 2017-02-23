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

    void Shoot()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            attackCooldown = timeBetweenAttacks;

        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Move();
	}

    void Move()
    {
        rigidbody.transform.position -= transform.right*speed*Time.deltaTime;
    }
}
