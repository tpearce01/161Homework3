using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase {
	public int phaseNumber;
	public int enemiesToSpawn;
	public float timeBetweenEnemiesMin;
	public float timeBetweenEnemiesMax;
	public List<EnemyToSpawn> enemies;

	public GameObject Spawn(){
		return enemies[Random.Range(0,enemies.Count)].Spawn();
	}
}
