using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    private bool playerExists;  //Ensures at least 1 player exists

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

    public abstract void InitializeLevel();
}
