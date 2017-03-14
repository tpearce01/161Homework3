using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    //Destroy the unit
    public override void OnKill()
    {
        int controllerNumber = gameObject.GetComponent<PlayerController>().controllerNumber;

        if (GameObject.Find("VictoryMenu") == null)
        {
            Level.i.lives[controllerNumber - 1]--;
            if (Level.i.lives[controllerNumber - 1] > 0)
            {
                if (controllerNumber == 1)
                {
                    Spawner.i.SpawnObject(Prefab.Player1, Vector3.zero);
                    HUDManager.i.UpdateLives(1);
                }
                else
                {
                    Spawner.i.SpawnObject(Prefab.Player2, Vector3.zero);
                    HUDManager.i.UpdateLives(2);
                }
            }
        }
    }
}
