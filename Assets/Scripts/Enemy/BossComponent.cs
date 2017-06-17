using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class BossComponent
 * 
 * Function: BossComponent class stores data about turrets to attach to the boss.
 *      *BossComponent and 
 *********************************************************************************/
[System.Serializable]
public class BossComponent
{
    public Vector3 location;
    public int maxHealth;
    public EnemyAttackType attackType;
    public float timeBetweenAttacks;
    public float attackCooldown;

    /// <summary>
    /// Generate a turret
    /// </summary>
    /// <returns></returns>
    public GameObject Spawn()
    {
        return Spawner.i.SpawnBossComponent(location, maxHealth, attackType, timeBetweenAttacks,
            attackCooldown);
    }
}
