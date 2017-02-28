using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0 : MonoBehaviour
{

    private float bossHealth;
    private float bossMaxHealth;
    public int speed;
    private int phase;


    void Awake()
    {
        bossMaxHealth = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<BossHealth>().bossHealthMax;
        bossHealth = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<BossHealth>().bossHealthCurrent;
    }

	// Use this for initialization
	void Start ()
	{

    }
	
	// Update is called once per frame
	void Update ()
	{
	    Move();
	    switch (phase)
	    {
            case 0:
                CheckPhase();
	            break;
            case 1:
                FinalPhase();
	            break;
            default:
                CheckDestroy();
	            break;
	    }
	}

    void Move()
    {
        if ((int)Time.time % 4 == 0 || (int)Time.time % 4 == 1)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y + Time.deltaTime * speed, gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y - Time.deltaTime * speed, gameObject.transform.position.z);
        }
    }

    void CheckPhase()
    {
        if ( Mathf.Approximately(bossHealth , bossMaxHealth /.5f))
        {
            phase = 1;
        }
    }

    void FinalPhase()
    {
        GameObject spawned = Spawner.i.SpawnBossComponent(Vector3.zero, 1000,EnemyAttackType.Spiral,.035f, 1, 0);
        spawned.transform.parent = gameObject.transform;
        spawned.transform.localPosition = Vector3.zero - transform.right*5 - transform.up*2;

        spawned = Spawner.i.SpawnBossComponent(Vector3.zero, 1000, EnemyAttackType.Spiral, .03f, 1, 0);
        spawned.transform.parent = gameObject.transform;
        spawned.transform.localPosition = Vector3.zero - transform.right * 5 + transform.up * 2;

        spawned = Spawner.i.SpawnBossComponent(Vector3.zero, 1000, EnemyAttackType.CircleSixtyFour, 5f, 1, 0);
        spawned.transform.parent = gameObject.transform;
        spawned.transform.localPosition = Vector3.zero - transform.right * 3;

        phase = 2;
    }

    void CheckDestroy()
    {
        if ( Mathf.Approximately(0f, bossHealth ))
        {
            Spawner.i.SpawnObject(Prefab.VictoryMenu, Vector3.zero);
            Destroy(gameObject);
        }
    }


}
