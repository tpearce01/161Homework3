using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class Unit
 * 
 * Function: Parent class to all generic units. A unit must have health and a 
 *      health slider.
 *********************************************************************************/
[System.Serializable]
public class EnemyToSpawn {
	public Vector3 locationMin;         //Minimum x and y spawn coordinates
	public Vector3 locationMax;         //Maximum x and y spawn coordinates
	public int maxHealth;               //Maximum health 
	public EnemyAttackType attackType;  //Attack pattern
	public float timeBetweenAttacks;    //Time between attacks
	public float attackCooldown;        //Time before first attack
	public int speed;                   //Movement speed

    /// <summary>
    /// Spawns the unit at the given coordinates
    /// </summary>
    /// <returns></returns>
	public GameObject Spawn(){
		return Spawner.i.SpawnCustomEnemy (new Vector3(Random.Range(locationMin.x, locationMax.x), Random.Range(locationMin.y, locationMax.y), Random.Range(locationMin.z, locationMax.z)), maxHealth, attackType, timeBetweenAttacks, attackCooldown, speed);
	}
}
