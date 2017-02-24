using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager i;                        //Static reference to GameManager
	List<GameObject> players = new List<GameObject>();  //List of players in the scene
    bool[] ready = new bool[2];                         //Determines which of the 2 players are ready

	void Awake () {
		if (GameObject.FindGameObjectsWithTag ("GameManager").Length > 1) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			i = this;
		}
	}

    //Adds a player to the list of players
	public void AddPlayer(GameObject p){
		players.Add (p);
	}

    //Remove a player from the list of players
	public void RemovePlayer(GameObject p){
		players.Remove (p);
	}

    //Returns all players in the scene
	public List<GameObject> GetPlayers(){
		return players;
	}

    public void ReadyUp(int index)
    {
        ready[index] = true;
    }

    public void NotReady(int index)
    {
        ready[index] = false;
    }

    public bool[] GetReady()
    {
        return ready;
    }
}
