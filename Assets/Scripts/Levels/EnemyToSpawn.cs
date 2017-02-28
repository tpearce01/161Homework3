using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyToSpawn {
	public Vector3 locationMin; 
	public Vector3 locationMax;
	public int maxHealth;
	public EnemyAttackType attackType;
	public float timeBetweenAttacks;
	public float attackCooldown;
	public int speed;


	public GameObject Spawn(){
		return Spawner.i.SpawnCustomEnemy (new Vector3(Random.Range(locationMin.x, locationMax.x), Random.Range(locationMin.y, locationMax.y), Random.Range(locationMin.z, locationMax.z)), maxHealth, attackType, timeBetweenAttacks, attackCooldown, speed);
	}
}
