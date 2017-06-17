using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class levelByPhase : Level
 * 
 * Function: Determines what happens in each phase of a level. Controls enemy
 *      spawns and types, time between each spawn, boss to spawn, and audio 
 *      to play for the level.
 *********************************************************************************/
public class LevelByPhase : Level {
	public float startDelay;	//Delay before starting spawns
	public List<Phase> phases;	//Details of each phase
	int phaseNumber;			//Current phase
	int spawned;				//Enemies spawned in current phase
	float timer;				//Time until next spawn
	public GameObject boss;		//Boss prefab. Every level must have a boss
    public Vector3 bossLocation;//Location to spawn boss
	bool bossSpawned;           //Determines if the boss has spawned
    public Sound audioToPlay;   //Background music to be played

    /// <summary>
    /// Start timer for spawning enemies and begin background music
    /// </summary>
	public override void InitializeLevel (){
		timer = startDelay;
	    if (audioToPlay != null)
	    {
	        SoundManager.i.PlaySoundLoop(audioToPlay, SoundManager.i.volume/2);
	    }
	}

    /// <summary>
    /// Controls all spawning and related timers.
    /// </summary>
	public override void UpdateLevel(){
		//Check all phases
		for (int i = 0; i < phases.Count; i++) {
			//If phase number matches current phase
			if (phaseNumber == phases [i].phaseNumber) {
				timer -= Time.deltaTime;
				//If it is time to spawn a new unit, spawn a new unit and check change phase
				if (timer <= 0) {
					//Spawn a new unit
					phases [i].Spawn ();
					timer = Random.Range (phases [i].timeBetweenEnemiesMin, phases [i].timeBetweenEnemiesMax);
					spawned++;
					//If it is time to change phases
					if (spawned >= phases [i].totalEnemiesToSpawn) {
						//Change phase
						NextPhase ();
					}
				}
				//If phase is found, there is no need to check additional phases
				break;
			}
		}

		//If all phases have passed,spawn the boss
		if (phaseNumber >= phases.Count && !bossSpawned && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
			Instantiate (boss).transform.position = bossLocation;
			bossSpawned = true;
		}
	}

	/// <summary>
    /// Increment phase and reset spawn counter
    /// </summary>
	void NextPhase(){
		phaseNumber++;
		spawned = 0;
	}
}

