using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public static Spawner i;								    //Static reference

	public GameObject[] prefabs;								//List of all prefabs that may be instantiated
	List<GameObject> activeObjects = new List<GameObject>();	//All active objects controlled by this script

	void Awake(){
		i = this;
	}

	void Update(){
		//Remove any objects that have been deleted
		activeObjects.RemoveAll(item => item == null);
	}

	//Instantiate an object at the specified location and add it to the list of active objects
	public void SpawnObject(int index, Vector3 location){
		activeObjects.Add(Instantiate (prefabs [index], location, Quaternion.identity) as GameObject);
	}
	public void SpawnObject(Prefab obj, Vector3 location){
		SpawnObject((int)obj, location);
	}

    //Instantiate an object at position with rotation
    public void SpawnObjectWithRotation(int index, Vector3 location, Vector3 rotation)
    {
        activeObjects.Add(Instantiate(prefabs[index], location, Quaternion.identity) as GameObject);
        activeObjects[activeObjects.Count - 1].transform.Rotate(rotation);
    }
    public void SpawnObjectWithRotation(Prefab obj, Vector3 location, Vector3 rotation)
    {
        SpawnObjectWithRotation((int)obj, location, rotation);
    }

    //Spawn a bullet with modified speed
    public void SpawnModifiedBullet(Prefab obj, Vector3 location, int speed)
    {
        SpawnModifiedBullet((int) obj, location, speed);
    }
    public void SpawnModifiedBullet(int index, Vector3 location, int speed)
    {
        activeObjects.Add(Instantiate(prefabs[index], location, Quaternion.identity) as GameObject);
        activeObjects[activeObjects.Count - 1].GetComponent<DefaultProjectile>().speed = speed;
    }

    //Spawn a rotated bullet with modified speed
    public void SpawnModifiedBulletRotated(Prefab obj, Vector3 location, int speed, Vector3 rotation)
    {
        SpawnModifiedBulletRotated((int)obj, location, speed, rotation);
    }
    public void SpawnModifiedBulletRotated(int index, Vector3 location, int speed, Vector3 rotation)
    {
        activeObjects.Add(Instantiate(prefabs[index], location, Quaternion.identity) as GameObject);
        activeObjects[activeObjects.Count - 1].GetComponent<DefaultProjectile>().speed = speed;
        activeObjects[activeObjects.Count - 1].transform.Rotate(rotation);
    }

	//Spawn object, rotated to face a random player. -transform.right faces the player. transform.right faces away from the player
	public void SpawnObjectTowardRandomPlayer(Prefab obj, Vector3 location){
		SpawnObjectTowardRandomPlayer ((int)obj, location);
	}
	public void SpawnObjectTowardRandomPlayer(int index, Vector3 location){
		activeObjects.Add(Instantiate(prefabs[index], location, Quaternion.identity) as GameObject);
		activeObjects [activeObjects.Count - 1].transform.right = activeObjects [activeObjects.Count - 1].transform.position - GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position;
	}

	//Spawn object, rotated to face a random player. transform.right faces the player
	public void SpawnObjectTowardRandomPlayerTrue(Prefab obj, Vector3 location){
		SpawnObjectTowardRandomPlayer ((int)obj, location);
	}
	public void SpawnObjectTowardRandomPlayerTrue(int index, Vector3 location){
		activeObjects.Add(Instantiate(prefabs[index], location, Quaternion.identity) as GameObject);
		activeObjects [activeObjects.Count - 1].transform.right = GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position - activeObjects [activeObjects.Count - 1].transform.position;
	}

	//Creates an enemy with customized Unit and EnemyBehavior fields. May also customize Sprite
	public GameObject SpawnCustomEnemy(Vector3 location, int maxHealth, EnemyAttackType attackType, float timeBetweenAttacks, float attackCooldown, int speed){
		GameObject spawned;
		activeObjects.Add(Instantiate(prefabs[(int)Prefab.Enemy], location, Quaternion.identity) as GameObject);
		spawned = activeObjects [activeObjects.Count - 1];
		spawned.GetComponent<Unit> ().maxHealth = maxHealth;
		spawned.GetComponent<Unit> ().ModifyHealth (maxHealth);
		spawned.GetComponent<EnemyBehavior> ().attackType = attackType;
		spawned.GetComponent<EnemyBehavior> ().timeBetweenAttacks = timeBetweenAttacks;
		spawned.GetComponent<EnemyBehavior> ().attackCooldown = attackCooldown;
		spawned.GetComponent<EnemyBehavior> ().speed = speed;
	    return spawned;
	}
	public GameObject SpawnCustomEnemy(Vector3 location, int maxHealth, EnemyAttackType attackType, float timeBetweenAttacks, float attackCooldown, int speed, Sprite sprite){
		GameObject spawned;
		activeObjects.Add(Instantiate(prefabs[(int)Prefab.Enemy], location, Quaternion.identity) as GameObject);
		spawned = activeObjects [activeObjects.Count - 1];
		spawned.GetComponent<Unit> ().maxHealth = maxHealth;
		spawned.GetComponent<Unit> ().ModifyHealth (maxHealth);
		spawned.GetComponent<EnemyBehavior> ().attackType = attackType;
		spawned.GetComponent<EnemyBehavior> ().timeBetweenAttacks = timeBetweenAttacks;
		spawned.GetComponent<EnemyBehavior> ().attackCooldown = attackCooldown;
		spawned.GetComponent<EnemyBehavior> ().speed = speed;
		spawned.GetComponent<SpriteRenderer> ().sprite = sprite;
	    return spawned;
	}

    public GameObject SpawnBossComponent(Vector3 location, int maxHealth, EnemyAttackType attackType, float timeBetweenAttacks, float attackCooldown, int speed)
    {
        GameObject spawned = SpawnCustomEnemy(location, maxHealth, attackType, timeBetweenAttacks, attackCooldown, speed);
        spawned.GetComponent<SpriteRenderer>().enabled = false;
        spawned.GetComponentInChildren<SpriteRenderer>().enabled = false;
        return spawned;
    }

	/*
	 * Sample of custom enemy spawns for easy testing
	 */ 
	public GameObject SpawnBasicEnemy(Vector3 location, EnemyAttackType at){
		return SpawnCustomEnemy (location, 100, at, 2, 2, 10);
	}
	public GameObject SpawnBasicEnemy(Vector3 location, int at){
		return SpawnCustomEnemy (location, 100, (EnemyAttackType)at, 2, 2, 10);
	}
	public GameObject SpawnSpiralEnemy(Vector3 location){
		return SpawnCustomEnemy (location, 100, EnemyAttackType.Spiral, 0.05f, 1, 10);
	}
}
	
//Enum to easily convert prefab names to the appropriate index
public enum Prefab{
	Shot1 = 0,
	Shot2 = 1,
	Enemy = 2,
    Player1 = 3,
    Player2 = 4,
	PauseMenu = 5,
	VictoryMenu = 6,
	FusedPlayer = 7,
    GameOverMenu = 8
};
