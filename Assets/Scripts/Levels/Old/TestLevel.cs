using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : Level
{

    public override void InitializeLevel()
    {
        //test
    }

    public override void UpdateLevel()
    {
        //test
    }

    void EndPhase()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Spawner.i.SpawnObject(Prefab.VictoryMenu, Vector3.zero);
        }
    }
}
