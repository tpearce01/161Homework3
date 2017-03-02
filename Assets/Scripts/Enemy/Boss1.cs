using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss0 {



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move(speed);

        print("Current Phase" + phase);
        switch(phase)
        {
            case 0:
                print("in phase 0");
                phase = CheckPhase(bossCurrentHealth, bossMaxHealth, .5f, phase);
                break;
            case 1:
                print("in phase 1");

                FinalPhase();
                break;
            default:
                CheckDestroy(bossCurrentHealth);
                break;
        }


	}

    void FinalPhase()
    {
        GameObject spawned = Spawner.i.SpawnBossComponent(Vector3.zero, 100, EnemyAttackType.Spiral, .035f, 1, 0);
        spawned.transform.parent = gameObject.transform;
        spawned.transform.localPosition = Vector3.zero - transform.right * 5 - transform.up * 2;
        spawned.gameObject.tag = "Turret";

        spawned = Spawner.i.SpawnBossComponent(Vector3.zero, 100, EnemyAttackType.Spiral, .03f, 1, 0);
        spawned.transform.parent = gameObject.transform;
        spawned.transform.localPosition = Vector3.zero - transform.right * 5 + transform.up * 2;
        spawned.gameObject.tag = "Turret";

        spawned = Spawner.i.SpawnBossComponent(Vector3.zero, 100, EnemyAttackType.CircleSixtyFour, 5f, 1, 0);
        spawned.transform.parent = gameObject.transform;
        spawned.transform.localPosition = Vector3.zero - transform.right * 3;
        spawned.gameObject.tag = "Turret";


        phase = 2;
    }
}
