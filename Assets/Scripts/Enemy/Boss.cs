﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public List<BossPhase> phases;
    private int currentPhase;
    public int speed;
    public Sprite bossSprite;
    private SpriteRenderer bossSpriteRenderer;

    void Start()
    {
        gameObject.transform.FindChild("BossSprite").GetComponent<SpriteRenderer>().sprite = bossSprite;
        NextPhase();
    }

	void Update ()
	{
	    Move();
	    if (gameObject.GetComponentsInChildren<Unit>().Length == 0)
	    {
	        NextPhase();
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

    void NextPhase()
    {
        if (currentPhase < phases.Count)
        {
            List<GameObject> spawnedComponents = phases[currentPhase].Spawn();
            for (int i = 0; i < spawnedComponents.Count; i++)
            {
                spawnedComponents[i].transform.parent = gameObject.transform;
                spawnedComponents[i].transform.localPosition = phases[currentPhase].enemies[i].location;
            }
            currentPhase++;
        }
        else
        {
            Spawner.i.SpawnObject(Prefab.VictoryMenu, Vector3.zero);
            Destroy(gameObject);
        }
    }
}