using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public static Spawner i;								    //Static reference

	public GameObject[] prefabs;								//List of all prefabs that may be instantiated
	GameObject activeObject;

	void Awake(){
		i = this;
	}

	//Instantiate an object at the specified location and add it to the list of active objects
	public void SpawnObject(int index, Vector3 location){
		activeObject = Instantiate (prefabs [index], location, Quaternion.identity) as GameObject;
	}
	public void SpawnObject(Prefab obj, Vector3 location){
		SpawnObject((int)obj, location);
	}

    //Instantiate an object at position with rotation
    public void SpawnObjectWithRotation(int index, Vector3 location, Vector3 rotation)
    {
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.transform.Rotate(rotation);
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
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.GetComponent<DefaultProjectile>().speed = speed;
    }

    //Spawn a rotated bullet with modified speed
    public void SpawnModifiedBulletRotated(Prefab obj, Vector3 location, int speed, Vector3 rotation)
    {
        SpawnModifiedBulletRotated((int)obj, location, speed, rotation);
    }
    public void SpawnModifiedBulletRotated(int index, Vector3 location, int speed, Vector3 rotation)
    {
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.GetComponent<DefaultProjectile>().speed = speed;
        activeObject.transform.Rotate(rotation);
    }

	//Spawn object, rotated to face a random player. -transform.right faces the player. transform.right faces away from the player
	public void SpawnObjectTowardRandomPlayer(Prefab obj, Vector3 location){
		SpawnObjectTowardRandomPlayer ((int)obj, location);
	}
	public void SpawnObjectTowardRandomPlayer(int index, Vector3 location){
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
		activeObject.transform.right = activeObject.transform.position - GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position;
	}

	//Spawn object, rotated to face a random player. transform.right faces the player
	public void SpawnObjectTowardRandomPlayerTrue(Prefab obj, Vector3 location){
		SpawnObjectTowardRandomPlayer ((int)obj, location);
	}
	public void SpawnObjectTowardRandomPlayerTrue(int index, Vector3 location){
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
		activeObject.transform.right = GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position - activeObject.transform.position;
	}

	//Creates an enemy with customized Unit and EnemyBehavior fields. May also customize Sprite
	public GameObject SpawnCustomEnemy(Vector3 location, int maxHealth, EnemyAttackType attackType, float timeBetweenAttacks, float attackCooldown, int speed){
		activeObject = Instantiate(prefabs[(int)Prefab.Enemy], location, Quaternion.identity) as GameObject;
		activeObject.GetComponent<Unit> ().maxHealth = maxHealth;
		activeObject.GetComponent<Unit> ().ModifyHealth (maxHealth);
		activeObject.GetComponent<EnemyBehavior> ().attackType = attackType;
		activeObject.GetComponent<EnemyBehavior> ().timeBetweenAttacks = timeBetweenAttacks;
		activeObject.GetComponent<EnemyBehavior> ().attackCooldown = attackCooldown;
		activeObject.GetComponent<EnemyBehavior> ().speed = speed;
		return activeObject;
	}
	public GameObject SpawnCustomEnemy(Vector3 location, int maxHealth, EnemyAttackType attackType, float timeBetweenAttacks, float attackCooldown, int speed, Sprite sprite){
		activeObject = Instantiate(prefabs[(int)Prefab.Enemy], location, Quaternion.identity) as GameObject;
		activeObject.GetComponent<Unit> ().maxHealth = maxHealth;
		activeObject.GetComponent<Unit> ().ModifyHealth (maxHealth);
		activeObject.GetComponent<EnemyBehavior> ().attackType = attackType;
		activeObject.GetComponent<EnemyBehavior> ().timeBetweenAttacks = timeBetweenAttacks;
		activeObject.GetComponent<EnemyBehavior> ().attackCooldown = attackCooldown;
		activeObject.GetComponent<EnemyBehavior> ().speed = speed;
		activeObject.GetComponent<SpriteRenderer> ().sprite = sprite;
		return activeObject;
	}

    public GameObject SpawnBossComponent(Vector3 location, int maxHealth, EnemyAttackType attackType, float timeBetweenAttacks, float attackCooldown, int speed)
    {
        activeObject = SpawnCustomEnemy(location, maxHealth, attackType, timeBetweenAttacks, attackCooldown, speed);
		activeObject.GetComponent<SpriteRenderer>().enabled = false;
		activeObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
		return activeObject;
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
