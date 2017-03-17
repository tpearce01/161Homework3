using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusedUnit : Unit {

	public override void OnKill(){
		List<GameObject> players = GameManager.i.GetPlayers();
		//Instantiate player1, player2
		Spawner.i.SpawnObject(Prefab.Player1, players[0].transform.position + transform.up);
		Spawner.i.SpawnObject(Prefab.Player2, players[0].transform.position - transform.up);

		//Calculate health
		Level.i.fusedHealth = 0;
		Level.i.fused = false;
	}
}
