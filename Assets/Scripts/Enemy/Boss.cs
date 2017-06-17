using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************************************************************
 * public class Boss
 * 
 * Purpose: Boss class stores all of the boss behaviors and phase information. It
 *      is responsible for calling the end of level, which is triggered when the
 *      last phase is completed
 *********************************************************************************/
public class Boss : MonoBehaviour
{
    public GameObject bossHealthPrefab;         //Prefab for boss healthbar
    private Slider bossHealth;                  //Slider for boss healthbar
    public List<BossPhase> phases;              //Contains all boss phase info *Should be private serializefield
    private int currentPhase;                   //Current phase number
    public int speed;                           //Boss movement speed *Should be private serializefield
    public Sprite bossSprite;                   //Sprite to apply to boss
    private SpriteRenderer bossSpriteRenderer;  //Boss' sprite renderer 

    //Initialize boss and begin phase 0
    void Start()
    {
        Initialize();
        NextPhase();
    }

    //Move boss and check for phase change
    void Update ()
	{
	    Move();
	    if (gameObject.GetComponentsInChildren<Unit>().Length == 0)
	    {
	        NextPhase();
	    }
	}

    /// <summary>
    /// - Initialize()
    /// Set boss sprite. Create and set boss health bar.
    /// </summary>
    void Initialize()
    {
        gameObject.transform.Find("BossSprite").GetComponent<SpriteRenderer>().sprite = bossSprite;
        bossHealth = Instantiate(bossHealthPrefab).transform.Find("Slider").GetComponent<Slider>();
    }

    /// <summary>
    /// - Move()
    /// Move the boss vertically, changing direction every 2 seconds.
    /// </summary>
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

    /// <summary>
    /// - NextPhase()
    /// Decrease boss health based on total number of phases.
    /// Spawn new boss components for the next phase.
    /// Increment phase counter.
    /// Checks for end of level.
    /// </summary>
    void NextPhase()
    {
        //Decrease boss health based on total number of phases
        bossHealth.value = (float) (phases.Count - currentPhase)/phases.Count;

        //Spawn new boss components for the next phase & Increment phase counter
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
        else //Check for end of level
        {
            Spawner.i.SpawnObject(Prefab.VictoryMenu, Vector3.zero);
            Destroy(gameObject);
        }
    }
}
