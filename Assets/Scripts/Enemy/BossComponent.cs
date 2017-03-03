using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossComponent
{
    public Vector3 location;
    public int maxHealth;
    public EnemyAttackType attackType;
    public float timeBetweenAttacks;
    public float attackCooldown;


    public GameObject Spawn()
    {
        return Spawner.i.SpawnBossComponent(location, maxHealth, attackType, timeBetweenAttacks,
            attackCooldown);
    }
}
