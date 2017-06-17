using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * Serializable class BossPhase
 * 
 * Function: Stores data relevant to initializing a boss phase. This includes the
 *      phase number and the list of components to spawn for that phase of combat
 *********************************************************************************/
[System.Serializable]
public class BossPhase
{
    public int phaseNumber;             //Phase number
    public List<BossComponent> enemies; //Components to spawn for this phase

    /// <summary>
    /// Spawn all boss components of the current phase
    /// </summary>
    /// <returns></returns>
    public List<GameObject> Spawn()
    {
        List<GameObject> toReturn = new List<GameObject>();
        for (int i = 0; i < enemies.Count; i++)
        {
            toReturn.Add(enemies[i].Spawn());
        }
        return toReturn;
    }
}
