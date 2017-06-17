using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*********************************************************************************
 * class Spawner
 * 
 * Function: Handles the instantiation of all prefabs
 *********************************************************************************/
public class Spawner : MonoBehaviour {

	public static Spawner i;			                            //Static reference

	public GameObject[] prefabs;								    //List of all prefabs that may be instantiated
	GameObject activeObject;                                        //Most recently spawned object

	//Pooled player bullets
	public List<GameObject> bulletsPlayer = new List<GameObject>(); //List of all player bullets
	public int bulletsPlayerToSpawn;                                //Number of bullets to spawn for the player

	//Pooled enemy bullets
	public List<GameObject> bulletsEnemy = new List<GameObject> (); //List of all enemy bullets
	public int bulletsEnemyToSpawn;                                 //Number of bullets to spawn for enemies

    //Get static reference
	void Awake(){
		i = this;
	}

	/// <summary>
    /// Generate object pool of bullets
    /// </summary>
	public void SpawnBullets(){
        //Player bullets
		for (int i = 0; i < bulletsPlayerToSpawn; i++) {
			GameObject obj = SpawnObject (Prefab.Shot1, Vector3.zero);
			obj.SetActive(false);
			bulletsPlayer.Add (obj);
		}

        //Enemy bullets
		for (int i = 0; i < bulletsEnemyToSpawn; i++) {
			GameObject obj = SpawnObject (Prefab.Shot2, Vector3.zero);
			obj.SetActive(false);
			bulletsEnemy.Add (obj);
		}
	}

	/// <summary>
    /// Spawn a single player bullet
    /// </summary>
    /// <param name="location"></param>
    /// <param name="player"></param>
	public void SpawnPlayerBullet(Vector3 location, int player){
		for(int i = 0; i < bulletsPlayer.Count; i++){
			if (!bulletsPlayer [i].activeInHierarchy) {
				bulletsPlayer [i].transform.position = location;
				bulletsPlayer [i].SetActive (true);
				bulletsPlayer [i].GetComponent<DefaultProjectile> ().player = player;
				break;
			}
		}
	}

    /// <summary>
    /// Spawn a single enemy bullet
    /// </summary>
    /// <param name="location"></param>
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

    /// <summary>
    /// Spawn a single enemy bullet at an angle
    /// </summary>
    /// <param name="location"></param>
    /// <param name="rotation"></param>
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

    /// <summary>
    /// Spawn a single enemy bullet directed at a random player
    /// </summary>
    /// <param name="location"></param>
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

	/// <summary>
    /// Instantiate a prefab at the specified location
    /// </summary>
    /// <param name="index"></param>
    /// <param name="location"></param>
    /// <returns></returns>
	public GameObject SpawnObject(int index, Vector3 location){
		activeObject = Instantiate (prefabs [index], location, Quaternion.identity) as GameObject;
		return activeObject;
	}

    /// <summary>
    /// Spawn a prefab at the specified location
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="location"></param>
    /// <returns></returns>
	public GameObject SpawnObject(Prefab obj, Vector3 location){
		return SpawnObject((int)obj, location);
	}

    /// <summary>
    /// Spawn a prefab at the specified location at an angle
    /// </summary>
    /// <param name="index"></param>
    /// <param name="location"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
	public GameObject SpawnObjectWithRotation(int index, Vector3 location, Vector3 rotation)
    {
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.transform.Rotate(rotation);
		return activeObject;
    }

    /// <summary>
    /// Spawn a prefab at the specified location at an angle
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="location"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
	public GameObject SpawnObjectWithRotation(Prefab obj, Vector3 location, Vector3 rotation)
    {
        return SpawnObjectWithRotation((int)obj, location, rotation);
    }

    /// <summary>
    /// Spawn a bullet with a modified speed
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="location"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
	public GameObject SpawnModifiedBullet(Prefab obj, Vector3 location, int speed)
    {
        return SpawnModifiedBullet((int) obj, location, speed);

    }

    /// <summary>
    /// Spawn a bullet with a modified speed
    /// </summary>
    /// <param name="index"></param>
    /// <param name="location"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
	public GameObject SpawnModifiedBullet(int index, Vector3 location, int speed)
    {
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.GetComponent<DefaultProjectile>().speed = speed;
		return activeObject;
    }

    /// <summary>
    /// Spawn a rotated bullet with a modified speed
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="location"></param>
    /// <param name="speed"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
	public GameObject SpawnModifiedBulletRotated(Prefab obj, Vector3 location, int speed, Vector3 rotation)
    {
        return SpawnModifiedBulletRotated((int)obj, location, speed, rotation);
    }

    /// <summary>
    /// Spawn a rotated bullet with a modified speed
    /// </summary>
    /// <param name="index"></param>
    /// <param name="location"></param>
    /// <param name="speed"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
	public GameObject SpawnModifiedBulletRotated(int index, Vector3 location, int speed, Vector3 rotation)
    {
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
        activeObject.GetComponent<DefaultProjectile>().speed = speed;
        activeObject.transform.Rotate(rotation);
		return activeObject;
    }

    /// <summary>
    /// Spawn object, rotated to face a random player. -transform.right faces the player. transform.right faces away from the player
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    public GameObject SpawnObjectTowardRandomPlayer(Prefab obj, Vector3 location){
		return SpawnObjectTowardRandomPlayer ((int)obj, location);
	}

    /// <summary>
    /// Spawn object, rotated to face a random player. -transform.right faces the player. transform.right faces away from the player
    /// </summary>
    /// <param name="index"></param>
    /// <param name="location"></param>
    /// <returns></returns>
	public GameObject SpawnObjectTowardRandomPlayer(int index, Vector3 location){
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
		activeObject.transform.right = activeObject.transform.position - GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position;
		return activeObject;
	}

    /// <summary>
    /// Spawn object, rotated to face a random player. transform.right faces the player
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    public GameObject SpawnObjectTowardRandomPlayerTrue(Prefab obj, Vector3 location){
		return SpawnObjectTowardRandomPlayer ((int)obj, location);
	}

    /// <summary>
    /// Spawn object, rotated to face a random player. transform.right faces the player
    /// </summary>
    /// <param name="index"></param>
    /// <param name="location"></param>
    /// <returns></returns>
	public GameObject SpawnObjectTowardRandomPlayerTrue(int index, Vector3 location){
		activeObject = Instantiate(prefabs[index], location, Quaternion.identity) as GameObject;
		activeObject.transform.right = GameManager.i.GetPlayers ()[Random.Range(0,GameManager.i.GetPlayers().Count)].transform.position - activeObject.transform.position;
		return activeObject;
	}

    /// <summary>
    /// Creates an enemy with customized Unit and EnemyBehavior fields. May also customize Sprite
    /// </summary>
    /// <param name="location"></param>
    /// <param name="maxHealth"></param>
    /// <param name="attackType"></param>
    /// <param name="timeBetweenAttacks"></param>
    /// <param name="attackCooldown"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Creates an enemy with customized Unit and EnemyBehavior fields. May also customize Sprite
    /// </summary>
    /// <param name="location"></param>
    /// <param name="maxHealth"></param>
    /// <param name="attackType"></param>
    /// <param name="timeBetweenAttacks"></param>
    /// <param name="attackCooldown"></param>
    /// <param name="speed"></param>
    /// <param name="sprite"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Spawn a boss component (Turret)
    /// </summary>
    /// <param name="location"></param>
    /// <param name="maxHealth"></param>
    /// <param name="attackType"></param>
    /// <param name="timeBetweenAttacks"></param>
    /// <param name="attackCooldown"></param>
    /// <returns></returns>
    public GameObject SpawnBossComponent(Vector3 location, int maxHealth, EnemyAttackType attackType, float timeBetweenAttacks, float attackCooldown)
    {
        activeObject = SpawnCustomEnemy(location, maxHealth, attackType, timeBetweenAttacks, attackCooldown, 0);
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
	Explosion = 10,
    TutorialCompleteMenu = 11
};
