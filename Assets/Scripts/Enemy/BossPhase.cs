using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossPhase
{
    public int phaseNumber;
    public List<BossComponent> enemies;

    //Spawns all boss components of the current phase
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
