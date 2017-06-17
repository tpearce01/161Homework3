using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class Phase
 * 
 * Function: Holds all information for a single phase of a level. Controls the
 *      number of enemies to spawn this phase, number of enemies to spawn per
 *      spawning, the minimim and maximum time between enemy spawns, and the
 *      types of enemies which can be spawned
 *********************************************************************************/
[System.Serializable]
public class Phase {
	public int phaseNumber;             //Phase number of this phase
	public int totalEnemiesToSpawn;     //Number of enemies to spawn before changing phases
	public int numToSpawnPerIteration;  //Number of enemies that spawn per spawn event
	public float timeBetweenEnemiesMin; //Minimum time until the next enemy
	public float timeBetweenEnemiesMax; //Maximum time until the next enemy
	public List<EnemyToSpawn> enemies;  //Enemies which can spawn in this phase

    /// <summary>
    /// Spawns a random enemy from the list of applicable enemies to spawn
    /// </summary>
	public void Spawn(){
		for (int i = 0; i < numToSpawnPerIteration; i++) {
			enemies [Random.Range (0, enemies.Count)].Spawn ();
		}
	}
}
