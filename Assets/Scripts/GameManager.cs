using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager i;
	List<GameObject> players = new List<GameObject>();

	// Use this for initialization
	void Awake () {
		if (GameObject.FindGameObjectsWithTag ("GameManager").Length > 1) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			i = this;
		}
	}

	public void AddPlayer(GameObject p){
		players.Add (p);
	}

	public void RemovePlayer(GameObject p){
		players.Remove (p);
	}

	public List<GameObject> GetPlayers(){
		return players;
	}
}
