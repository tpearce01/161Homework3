using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    private bool playerExists;  //Ensures at least 1 player exists
	List<float> storedPlayerHealth = new List<float>();
    private bool fused;
    private bool gameOver;

    void Start()
    {
        if (GameManager.i.GetReady()[0])
        {
            Spawner.i.SpawnObject(Prefab.Player1, new Vector3(-10,5,0));
            playerExists = true;
        }
        if (GameManager.i.GetReady()[1])
        {
            Spawner.i.SpawnObject(Prefab.Player2, new Vector3(-10, -5, 0));
            playerExists = true;
        }

        //If no player exists, default to player 1
        if (!playerExists)
        {
            Spawner.i.SpawnObject(Prefab.Player1, new Vector3(-10, 5, 0));
        }

        InitializeLevel();
    }

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Spawner.i.SpawnObject (Prefab.PauseMenu, Vector3.zero);
		}
	    if (fused)
	    {
	        CheckUnfuse();
	    }
	    else
	    {
	        CheckFusion();
	    }
	    UpdateLevel ();
	}

    void LateUpdate()
    {
        CheckGameOver();
    }

    void CheckFusion(){
	    bool canFuse = true;
	    for (int i = 0; i < GameManager.i.GetPlayers().Count; i++)
	    {
	        if (!GameManager.i.GetPlayers()[i].GetComponent<PlayerController>().readyToFuse)
	        {
	            canFuse = false;
	            break;
	        }
	    }

	    if (canFuse)
	    {
	        //Check the distance between the players
	        Vector2 originalPosition = GameManager.i.GetPlayers()[0].transform.position;
	        bool initiateFusion = true;
	        for (int i = 1; i < GameManager.i.GetPlayers().Count; i++)
	        {
	            if (Vector2.Distance(originalPosition, GameManager.i.GetPlayers()[i].transform.position) > 2)
	            {
	                initiateFusion = false;
	                break;
	            }
	        }
	        if (initiateFusion)
	        {
	            for (int i = 0; i < GameManager.i.GetPlayers().Count; i++)
	            {
	                GameManager.i.GetPlayers()[i].GetComponent<PlayerController>().readyToFuse = false;
	            }
	            Fuse();
	        }
	    }
	}

	void Fuse(){
		//Instantiate fused ship at player 1 location
		Spawner.i.SpawnObject (Prefab.FusedPlayer, GameManager.i.GetPlayers () [0].transform.position);

        //Calculate health
        float totalHealth = 0;	//Total health of all players except fused player
		for (int i = 0; i < GameManager.i.GetPlayers ().Count - 1; i++) {
			totalHealth += GameManager.i.GetPlayers () [i].GetComponent<Unit> ().health;
		}
		GameManager.i.GetPlayers()[GameManager.i.GetPlayers().Count-1].GetComponent<Unit>().health = totalHealth/GameManager.i.GetPlayers().Count;

		//Remove players
		for (int i = GameManager.i.GetPlayers ().Count - 1; i >= 0; i--) {
			storedPlayerHealth.Add (GameManager.i.GetPlayers () [i].GetComponent<Unit> ().health);
		    GameObject toDestroy = GameManager.i.GetPlayers()[i];
			GameManager.i.RemovePlayer (GameManager.i.GetPlayers () [i]);
		    Destroy(toDestroy);
        }

        fused = true;
	}

	void Unfuse(){
		//Instantiate player1, player2
		Spawner.i.SpawnObject(Prefab.Player1, GameManager.i.GetPlayers()[0].transform.position + transform.up);
		Spawner.i.SpawnObject(Prefab.Player2, GameManager.i.GetPlayers()[0].transform.position - transform.up);

		//Calculate health
		for (int i = 1; i < GameManager.i.GetPlayers ().Count; i++) {
			GameManager.i.GetPlayers () [i].GetComponent<Unit> ().health = GameManager.i.GetPlayers () [0].GetComponent<Unit> ().health;
		}

		//Remove fused ship
	    GameObject toDesroy = GameManager.i.GetPlayers()[0];
		GameManager.i.RemovePlayer(GameManager.i.GetPlayers()[0]);
        Destroy(toDesroy);

	    fused = false;
	}

    void CheckUnfuse()
    {
        if (Input.GetButtonDown("Y1") || Input.GetButtonDown("Y2")/*TESTING*/ || Input.GetKeyDown(KeyCode.Y)/*TESTING*/ )
        {
            Unfuse();
        }
    }

    void CheckGameOver()
    {
        if (!gameOver && GameManager.i.GetPlayers().Count == 0)
        {
            Spawner.i.SpawnObject(Prefab.GameOverMenu, Vector3.zero);
            gameOver = true;
        }
    }

    public abstract void InitializeLevel();
	public abstract void UpdateLevel ();


}
