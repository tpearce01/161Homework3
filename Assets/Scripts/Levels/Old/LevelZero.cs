using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelZero : Level {

	int phase;
	int[] phaseChange;
	float timer;
	int spawned;

	public override void UpdateLevel(){
		switch (phase) {
		case 0:
			PhaseZero ();
			break;
		case 1:
			PhaseOne ();
			break;
		case 2:
			PhaseTwo ();
			break;
        case 3:
            PhaseThree();
		    break;
        case 4:
            PhaseFour();
		    break;
		default:
			EndPhase ();
			break;
		}
	}

    public override void InitializeLevel()
    {
		phaseChange = new int[]{5, 10, 20, 50, 80};
		phase = 0;	//Begin at phase 0
		timer = 3;	//3s before phase 0 begins
    }

	//Spawn basic enemy every 3-4 seconds
	void PhaseZero(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = Random.Range (3, 5);	//Reset timer to 3 or 4
			Spawner.i.SpawnBasicEnemy(new Vector3(40, Random.Range(-17, 20), 0), Random.Range((int)EnemyAttackType.StandardSingle, (int)EnemyAttackType.DirectedSingle + 1));
			spawned++;
			if (spawned > phaseChange[0]) {
				phase = 1;
			}
		}
	}

	//Spawn basic or directed enemy every 3-4 seconds
	void PhaseOne(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = Random.Range (3, 5);	//Reset timer to 3 or 4
			Spawner.i.SpawnBasicEnemy(new Vector3(40, Random.Range(-17, 20), 0), Random.Range((int)EnemyAttackType.StandardSingle, (int)EnemyAttackType.CircleEight + 1));
			spawned++;
			if (spawned > phaseChange[1]) {
				phase = 2;
			}
		}
	}

	void PhaseTwo(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = Random.Range (1, 3);	//Reset timer to 1 or 2
			Spawner.i.SpawnBasicEnemy(new Vector3(40, Random.Range(-17, 20), 0), Random.Range((int)EnemyAttackType.StandardSingle, (int)EnemyAttackType.CircleEight + 1));
			spawned++;
			if (spawned > phaseChange[2]) {
				phase = 3;
			}
		}
	}

	void PhaseThree(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = Random.Range (0, 10) / 10f; //Reset timer to a random float between 0 and .9
            Spawner.i.SpawnBasicEnemy(new Vector3(40, Random.Range(-17, 20), 0), Random.Range((int)EnemyAttackType.StandardSingle, (int)EnemyAttackType.CircleEight + 1));
            spawned++;
			if (spawned > phaseChange[3]) {
				phase = 4;
			}
		}
	}

    /*
	void PhaseFour(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = Random.Range (0, 10) / 10f;	//Reset timer to a random float between 0 and .9
			Spawner.i.SpawnSpiralEnemy(new Vector3(40, Random.Range(-17, 20), 0));
			spawned++;
			if (spawned > phaseChange[4]) {
				phase = 5;
			}
		}
	}
    */

    void PhaseFour()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = Random.Range(0, 10) / 10f;  //Reset timer to a random float between 0 and .9
            switch (Random.Range(0, 3))
            {
                case 0:
                case 1:
                    Spawner.i.SpawnBasicEnemy(new Vector3(40, Random.Range(-17, 20), 0), Random.Range((int)EnemyAttackType.StandardSingle, (int)EnemyAttackType.CircleEight + 1));
                    break;
                case 2:
                    Spawner.i.SpawnSpiralEnemy(new Vector3(40, Random.Range(-17, 20), 0));
                    break;
                default:
                    Spawner.i.SpawnBasicEnemy(new Vector3(40, Random.Range(-17, 20), 0), Random.Range((int)EnemyAttackType.StandardSingle, (int)EnemyAttackType.CircleEight + 1));
                    break;
            }
            spawned++;
            if (spawned > phaseChange[4])
            {
                phase = 5;
            }
        }
    }

	void EndPhase(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
				timer = int.MaxValue;
				Spawner.i.SpawnObject (Prefab.VictoryMenu, Vector3.zero);
			}
		}
	}

}
