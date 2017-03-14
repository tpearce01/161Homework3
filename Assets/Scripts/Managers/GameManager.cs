﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager i;                        //Static reference to GameManager
	List<GameObject> players = new List<GameObject>();  //List of players in the scene
    bool[] ready;                         //Determines which of the 2 players are ready
    public int level;
	public int[] score;

	void Awake () {
		if (GameObject.FindGameObjectsWithTag ("GameManager").Length > 1) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			i = this;
		    level = 1;
		    score = new int[2];
			ready = new bool[2];   
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
		if (!ready [0] && !ready [1]) {
			ready [0] = true;
		}
		return ready;
    }

	public void ResetScore(){
		score = new int[2];
	}

}
