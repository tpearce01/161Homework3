using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public static Spawner i;								    //Static reference

	public GameObject[] prefabs;								//List of all prefabs that may be instantiated
	GameObject activeObject;

	//Pooled player bullets
	public List<GameObject> bulletsPlayer = new List<GameObject>();
	public int bulletsPlayerToSpawn;
	//int bulletsPlayerIndex;

	//Pooled enemy bullets
	public List<GameObject> bulletsEnemy = new List<GameObject> ();
	public int bulletsEnemyToSpawn;
	//int bulletsEnemyIndex;

	void Awake(){
		i = this;
	}

	//Spawn object pool of bullets
	public void SpawnBullets(){
		for (int i = 0; i < bulletsPlayerToSpawn; i++) {
			GameObject obj = SpawnObject (Prefab.Shot1, Vector3.zero);
			obj.SetActive(false);
			bulletsPlayer.Add (obj);
		}
		for (int i = 0; i < bulletsEnemyToSpawn; i++) {
			GameObject obj = SpawnObject (Prefab.Shot2, Vector3.zero);
			obj.SetActive(false);
			bulletsEnemy.Add (obj);
		}
	}

	//Pooled spawn functions
	public void SpawnPlayerBullet(Vector3 location){
		for(int i = 0; i < bulletsPlayer.Count; i++){
			if (!bulletsPlayer [i].activeInHierarchy) {
				bulletsPlayer [i].transform.position = location;
				bulletsPlayer [i].SetActive (true);
				break;
			}
		}
	}
	public void SpawnEnemyBullet(Vector3 location){
		for (int i = 0; i < bulletsEnemy.Count; i++) {
			if (!bulletsEnemy [i].activeInHierarchy) {
				bulletsEnemy [i].transform.position = location;
				bulletsEnemy [i].transform.rotation = new Quaternion(0,0,0,0);
				bulletsEnemy [i].SetActive (true);
				break;
			}
		}
	}
	public void SpawnEnemyBulletWithRotation(Vector3 location, Vector3 rotation){
		for (int i = 0; i < bulletsEnemyToSpawn; i++) {
			if (!bulletsEnemy [i].activeInHierarchy) {
				bulletsEnemy [i].transform.position = location;
				bulletsEnemy [i].transform.rotation = new Quaternion(0,0,0,0);
				bulletsEnemy [i].transform.Rotate (rotation);
				bulletsEnemy [i].SetActive (true);
				break;
			}
		}
	}
	public void SpawnEnemyBulletTowardPlayer(Vector3 location){
		for (int i = 0; i < bulletsEnemyToSpawn; i++) {
			if (!bulletsEnemy [i].activeInHierarchy) {
				bulletsEnemy [i].transform.position = location;
				bulletsEnemy [i].transform.rotation = new Quaternion(0,0,0,0);
				bulletsEnemy[i].transform.right = bulletsEnemy[i].transform.position - GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position;
				bulletsEnemy [i].SetActive (true);
				break;
			}
		}
	}

	//Instantiate an object at the specified location and add it to the list of active objects
	public GameObject SpawnObject(int index, Vector3 location){
		activeObject = Instantiate (prefabs [index], location, Quaternion.identity) as GameObject;
		return activeObject;
	}
	public GameObject SpawnObject(Prefab obj, Vector3 location){
		return SpawnObject((int)obj, location);
	}

    //Instantiate an object at position with rotation
	public GameObject SpawnObjectWithRotation(int index, Vector3 location, Vector3 rotation)
    {
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.transform.Rotate(rotation);
		return activeObject;
    }
	public GameObject SpawnObjectWithRotation(Prefab obj, Vector3 location, Vector3 rotation)
    {
        return SpawnObjectWithRotation((int)obj, location, rotation);
    }

    //Spawn a bullet with modified speed
	public GameObject SpawnModifiedBullet(Prefab obj, Vector3 location, int speed)
    {
        return SpawnModifiedBullet((int) obj, location, speed);

    }
	public GameObject SpawnModifiedBullet(int index, Vector3 location, int speed)
    {
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.GetComponent<DefaultProjectile>().speed = speed;
		return activeObject;
    }

    //Spawn a rotated bullet with modified speed
	public GameObject SpawnModifiedBulletRotated(Prefab obj, Vector3 location, int speed, Vector3 rotation)
    {
        return SpawnModifiedBulletRotated((int)obj, location, speed, rotation);
    }
	public GameObject SpawnModifiedBulletRotated(int index, Vector3 location, int speed, Vector3 rotation)
    {
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.GetComponent<DefaultProjectile>().speed = speed;
        activeObject.transform.Rotate(rotation);
		return activeObject;
    }

	//Spawn object, rotated to face a random player. -transform.right faces the player. transform.right faces away from the player
	public GameObject SpawnObjectTowardRandomPlayer(Prefab obj, Vector3 location){
		return SpawnObjectTowardRandomPlayer ((int)obj, location);
	}
	public GameObject SpawnObjectTowardRandomPlayer(int index, Vector3 location){
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
		activeObject.transform.right = activeObject.transform.position - GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position;
		return activeObject;
	}

	//Spawn object, rotated to face a random player. transform.right faces the player
	public GameObject SpawnObjectTowardRandomPlayerTrue(Prefab obj, Vector3 location){
		return SpawnObjectTowardRandomPlayer ((int)obj, location);
	}
	public GameObject SpawnObjectTowardRandomPlayerTrue(int index, Vector3 location){
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
		activeObject.transform.right = GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position - activeObject.transform.position;
		return activeObject;
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
    GameOverMenu = 8,
	Sparks = 9,
	Explosion = 10
};
