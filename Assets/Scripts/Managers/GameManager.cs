using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class GameManager
 * 
 * Function: 
 *********************************************************************************/
public class GameManager : MonoBehaviour {

	public static GameManager i;                        //Static reference to GameManager
	List<GameObject> players = new List<GameObject>();  //List of players in the scene
    bool ready;                                         //Determines if the player is ready
    public int level;                                   //Current level, begins at level 1
	public int score;                                   //Total player score

    //Ensures there are no duplicate GameManager objects then initializes persistent 
    //game variables
	void Awake () {
		if (GameObject.FindGameObjectsWithTag ("GameManager").Length > 1) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			i = this;
		    level = 1;
		    score = 0;
			ready = false;   
		}
	}

    /// <summary>
    /// Adds a player to the list of players.
    /// </summary>
    /// <param name="p"></param>
	public void AddPlayer(GameObject p){
		players.Add (p);
	}

    /// <summary>
    /// Remove a player from the list of players
    /// </summary>
    /// <param name="p"></param>
	public void RemovePlayer(GameObject p){
		players.Remove (p);
	}

    /// <summary>
    /// Gets all players
    /// </summary>
    /// <returns></returns>
	public List<GameObject> GetPlayers(){
		return players;
	}

    /// <summary>
    /// Sets the player ready status to ready
    /// </summary>
    public void ReadyUp()
    {
        ready = true;
    }

    /// <summary>
    /// Sets the player ready status to not ready
    /// </summary>
    /// <param name="index"></param>
    public void NotReady(int index)
    {
        ready = false;
    }

    /// <summary>
    /// Returns the player ready status
    /// </summary>
    /// <returns></returns>
    public bool GetReady()
    {
		return ready;
    }

    /// <summary>
    /// Resets the score to 0
    /// </summary>
	public void ResetScore(){
		score = 0;
	}

}
