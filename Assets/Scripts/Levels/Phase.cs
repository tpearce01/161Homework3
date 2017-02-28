using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase {
	public int phaseNumber;
	public int totalEnemiesToSpawn;
	public int numToSpawnPerIteration;
	public float timeBetweenEnemiesMin;
	public float timeBetweenEnemiesMax;
	public List<EnemyToSpawn> enemies;

	public void Spawn(){
		for (int i = 0; i < numToSpawnPerIteration; i++) {
			enemies [Random.Range (0, enemies.Count)].Spawn ();
		}
	}
}
