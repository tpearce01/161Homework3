using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public static Level i;
    private bool playerExists;  //Ensures at least 1 player exists
	//public List<float> storedPlayerHealth = new List<float>();
    public float totalHealth;
    public float fusedHealth;
    private bool fused;
    private bool gameOver;
    public int[] playerScore;

    void Awake()
    {
        playerScore = new int[2];
        i = this;
        totalHealth = 200;
        fusedHealth = 100;
        //storedPlayerHealth.Add(100f);
        //storedPlayerHealth.Add(100f);
    }

    void Start()
    {
        if (GameManager.i.GetReady()[0])
        {
            Spawner.i.SpawnObject(Prefab.Player1, new Vector3(-10,5,0));
            i.playerScore[0] = 0;
            playerExists = true;
        }
        if (GameManager.i.GetReady()[1])
        {
            Spawner.i.SpawnObject(Prefab.Player2, new Vector3(-10, -5, 0));
            playerExists = true;
            i.playerScore[1] = 0;
        }

        //If no player exists, default to player 1
        if (!playerExists)
        {
            Spawner.i.SpawnObject(Prefab.Player1, new Vector3(-10, 5, 0));
            i.playerScore[0] = 0;
        }
		Spawner.i.SpawnBullets ();
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
        if (GameManager.i.GetPlayers().Count > 1)
        {
            bool canFuse = true;
			List<GameObject> players = GameManager.i.GetPlayers ();
            for (int i = 0; i < GameManager.i.GetPlayers().Count; i++)
            {
				if (!players[i].GetComponent<PlayerController>().readyToFuse)
                {
                    canFuse = false;
                    break;
                }
            }


            if (canFuse)
            {
                Vector2 originalPosition = Vector2.zero;
                //Check the distance between the players
				if (players.Count > 0)
                {
					originalPosition = players[0].transform.position;
                }

                bool initiateFusion = true;
				for (int i = 1; i < players.Count; i++)
                {
					if (Vector2.Distance(originalPosition, players[i].transform.position) > 2)
                    {
                        initiateFusion = false;
                        break;
                    }
                }
                if (initiateFusion)
                {
					for (int i = 0; i < players.Count; i++)
                    {
						players[i].GetComponent<PlayerController>().readyToFuse = false;
                    }
                    Fuse();
                }
            }
        }
    }

	void Fuse(){
        //Instantiate fused ship at player 1 location
        Spawner.i.SpawnObject (Prefab.FusedPlayer, GameManager.i.GetPlayers () [0].transform.position);

		List<GameObject> players = GameManager.i.GetPlayers ();
			
        //Calculate health
	    totalHealth = 0;
		for (int i = 0; i < players.Count; i++) {
			totalHealth += players[i].GetComponent<Unit> ().health;
		}

		//Remove players
		for (int i = players.Count - 1; i >= 0; i--) {
			//storedPlayerHealth.Add (players[i].GetComponent<Unit> ().health);
		    GameObject toDestroy = GameManager.i.GetPlayers()[i];
			GameManager.i.RemovePlayer (GameManager.i.GetPlayers () [i]);
		    Destroy(toDestroy);
        }

        fused = true;
	}

	public void Unfuse(){
		List<GameObject> players = GameManager.i.GetPlayers ();
		//Instantiate player1, player2
		Spawner.i.SpawnObject(Prefab.Player1, players[0].transform.position + transform.up);
		Spawner.i.SpawnObject(Prefab.Player2, players[0].transform.position - transform.up);

		//Calculate health
	    fusedHealth = GameManager.i.GetPlayers()[0].GetComponent<Unit>().health;

		//Remove fused ship
	    GameObject toDesroy = GameManager.i.GetPlayers()[0];
		GameManager.i.RemovePlayer(GameManager.i.GetPlayers()[0]);
        Destroy(toDesroy);

	    fused = false;
	}

    public void FusedDeath()
    {
        List<GameObject> players = GameManager.i.GetPlayers();
        //Instantiate player1, player2
        Spawner.i.SpawnObject(Prefab.Player1, players[0].transform.position + transform.up);
        Spawner.i.SpawnObject(Prefab.Player2, players[0].transform.position - transform.up);

        //Calculate health
        fusedHealth = GameManager.i.GetPlayers()[0].GetComponent<Unit>().health;
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

    public void updateScore(int player , int scoreInc)
    {
        i.playerScore[player] += scoreInc;
       // Debug.Log(i.playerScore[player]);
    }
    public abstract void InitializeLevel();
	public abstract void UpdateLevel ();


}
