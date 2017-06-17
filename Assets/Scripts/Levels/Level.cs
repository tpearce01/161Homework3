using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * abstract class Level
 * 
 * Function: Level class holds all vital functions and data relevant to creating
 *      and running a level. Parent to class LevelByPhase. 
 *********************************************************************************/
public abstract class Level : MonoBehaviour
{
    public static Level i;      //Allows for quick referencing to the current level
    private bool playerExists;  //Ensures at least 1 player exists
    private bool gameOver;      //Determines when the game has ended
    public int playerScore;   //Tracks player scores
    public int lives;         //Tracks player lives
    [SerializeField] private int startingLives = 3;

    // Reset player score and lives. Get static reference to current level.
    void Awake()
    {
        playerScore = 0;
        lives = startingLives;
        i = this;
    }

    // Spawn the player. Spawn object pool of bullets. Initialize level.
    void Start()
    {
        //Spawn player 1
        Spawner.i.SpawnObject(Prefab.Player1, new Vector3(-10,5,0));
        playerExists = true;


        //If no player exists, default to player 1
        if (!playerExists)
        {
            Spawner.i.SpawnObject(Prefab.Player1, new Vector3(-10, 5, 0));
            playerExists = true;
        }

        //Spawn object pool
		Spawner.i.SpawnBullets ();

        //Initialize level
        InitializeLevel();
    }

    //Check for pause input
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown("Start1")) {
			Spawner.i.SpawnObject (Prefab.PauseMenu, Vector3.zero);
		}
	    UpdateLevel ();
	}

    //Check for game over condition
    void LateUpdate()
    {
        CheckGameOver();
    }

    /// <summary>
    /// Checks if the game should end
    /// </summary>
    void CheckGameOver()
    {
        if (!gameOver && lives <= 0)
        {
            Spawner.i.SpawnObject(Prefab.GameOverMenu, Vector3.zero);
            gameOver = true;
        }
    }

    /// <summary>
    /// Update the player's score
    /// </summary>
    /// <param name="player"></param>
    /// <param name="scoreInc"></param>
    public void updateScore(int scoreInc)
    {
        i.playerScore += scoreInc;
    }

    //Abstract functions for custom initialization and updates
    public abstract void InitializeLevel();
	public abstract void UpdateLevel ();


}
