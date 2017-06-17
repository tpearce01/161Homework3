using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class PlauerUnit : Unit
 * 
 * Function: Extends unit functionality for custom OnKill() function. Child of 
 *       Unit class.
 *********************************************************************************/
public class PlayerUnit : Unit
{
    /// <summary>
    /// Custom OnKill function. Respawns the player if they are dead. Reduced functionality
    /// to single player
    /// </summary>
    public override void OnKill()
    {
        int controllerNumber = gameObject.GetComponent<PlayerController>().controllerNumber;

        if (GameObject.Find("VictoryMenu") == null)
        {
            Level.i.lives--;
            if (Level.i.lives > 0)
            {
                //if (controllerNumber == 1)
                //{
                    Spawner.i.SpawnObject(Prefab.Player1, Vector3.zero);
                    HUDManager.i.UpdateLives(1);
                /*}
                else
                {
                    Spawner.i.SpawnObject(Prefab.Player2, Vector3.zero);
                    HUDManager.i.UpdateLives(2);
                }*/
            }
        }
    }
}
