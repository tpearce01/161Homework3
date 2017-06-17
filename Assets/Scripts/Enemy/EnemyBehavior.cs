using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class EnemyBehavior
 * 
 * Function: Controls the behavior of enemy units including turrets and standard
 *      enemies. Controlled behaviors are attack pattern, attack frequency, time
 *      before first attack, and movement speed
 *********************************************************************************/
public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody2D rigidbody;      //Quick reference to this objects rigidbody
	public EnemyAttackType attackType;  //Attack pattern
    public float timeBetweenAttacks;    //Attack frequency
    public float attackCooldown;        //Time before first attack
    public float speed;                 //Movement speed
	private int spiralDegree;           //Tracks degree for spiral attack patterns

    //Get rigidbody reference and randomize speed by +/- 30%
	void Start ()
	{
	    rigidbody = gameObject.GetComponent<Rigidbody2D>();
	    speed = speed*Random.Range(0.7f, 1.3f);
	}

    //Move every frame
	void FixedUpdate ()
	{
	    Move();				
	}

    //Check for next attack
	void Update(){
		Shoot ();
	}

	/// <summary>
    /// Moves the unit to the left at a rate modified by speed variable
    /// </summary>
    void Move()
    {
        rigidbody.transform.position -= transform.right*speed*Time.deltaTime;
    }

    /// <summary>
    /// Decrement time until next attack, or perform an attack if it is time to do so
    /// </summary>
	void Shoot(){
		if (attackCooldown > 0) {
			attackCooldown -= Time.deltaTime;
		} else {
			attackCooldown = timeBetweenAttacks;
			switch ((int)attackType) {
			case 0:	//Standard Single
				StandardSingle ();
				break;
			case 1:	//Directed Single
				DirectedSingle ();
				break;
			case 2:	//Random Single
				RandomSingle ();
				break;
			case 3:	//Circle of 8
				CircleDynamic (8);
				break;
			case 4:	//Circle of 4
				CircleDynamic (4);
				break;
			case 5:	//Offset Circle of 4
				OffsetCircleDynamic(4);
				break;
			case 6:
				Spiral ();
				break;
            case 7:
                CircleDynamic(32);
			    break;
            case 8:
                CircleDynamic(64);
			    break;
			case 9:
				SpiralWiggle ();
				break;
			case 10:
				SpiralWiggle (16);
				break;
			case 11:
				CircleRandom (4);
				break;
			case 12:
				CircleRandom (8);
				break;
			case 13:
				CircleRandom (16);
				break;
			default:
				StandardSingle ();
				break;
			}
		}
	}

	/// <summary>
    /// Single bullet fired straight
    /// </summary>
	void StandardSingle()
	{
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnEnemyBullet (gameObject.transform.position);
	}

	/// <summary>
    /// Single bullet fired at the player
    /// </summary>
	void DirectedSingle(){
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnEnemyBulletTowardPlayer (gameObject.transform.position);
	}

	/// <summary>
    /// 2 bullets fired straight
    /// </summary>
	void StandardDouble(){
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnEnemyBullet (gameObject.transform.position + transform.up);
		Spawner.i.SpawnEnemyBullet (gameObject.transform.position - transform.up);
	}

	/// <summary>
    /// 1 bullet at a random direction
    /// </summary>
	void RandomSingle(){
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnEnemyBulletWithRotation (gameObject.transform.position, new Vector3(0,0,Random.Range(-180,181)));
	}

	/// <summary>
    /// Spawn a variable number of bullets spaced evenly in a circle
    /// </summary>
    /// <param name="bullets"></param>
	void CircleDynamic(int bullets){
		for (int i = 0; i < bullets; i++) {
			Spawner.i.SpawnEnemyBulletWithRotation(gameObject.transform.position, new Vector3(0,0,i * 360 / bullets));
		}
	}

	/// <summary>
    /// Spawns a variable number of bullets spaced evenly in an offset circle
    /// </summary>
    /// <param name="bullets"></param>
	void OffsetCircleDynamic(int bullets){
		for (int i = 0; i < bullets; i++) {
			Spawner.i.SpawnEnemyBulletWithRotation(gameObject.transform.position, new Vector3(0,0,i * 360 / bullets + 360 / bullets / 2));
		}
	}

	/// <summary>
    /// Fire a single bullet sequentially in a spiral pattern
    /// </summary>
	void Spiral(){
		Spawner.i.SpawnEnemyBulletWithRotation(gameObject.transform.position, new Vector3(0,0,spiralDegree));
		spiralDegree += 15;
	}

    /// <summary>
    /// Fires a single bullet sequentially in a zig-zag pattern
    /// </summary>
	void SpiralWiggle(){
		Spawner.i.SpawnEnemyBulletWithRotation(gameObject.transform.position, new Vector3(0,0,spiralDegree));
		if ((int)Time.time % 2 == 0) {
			spiralDegree += 1;
		} else {
			spiralDegree -= 1;
		}
	}

    /// <summary>
    /// Fires a variable number of bullets sequentailly in a zig-zag pattern
    /// </summary>
    /// <param name="bullets"></param>
	void SpiralWiggle(int bullets){
		for (int i = 0; i < bullets; i++) {
			Spawner.i.SpawnEnemyBulletWithRotation(gameObject.transform.position, new Vector3(0,0,(i * 135 / bullets + 135 / bullets / 2) + spiralDegree - 45));
		}
		if ((int)Time.time % 2 == 0) {
			spiralDegree += 1;
		} else {
			spiralDegree -= 1;
		}
	}

    /// <summary>
    /// Fires a variable number of bullets in a randomly offset circle pattern
    /// </summary>
    /// <param name="bullets"></param>
	void CircleRandom(int bullets){
		int offset = Random.Range (0, 360);
		for (int i = 0; i < bullets; i++) {
			Spawner.i.SpawnEnemyBulletWithRotation(gameObject.transform.position, new Vector3(0,0,(i * 360 / bullets + 360 / bullets / 2) + offset));
		}
	}

	//If the unit collides with the player, destroy it
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("Player")){
			other.gameObject.GetComponent<Unit>().ModifyHealth(-20);
			SoundManager.i.PlaySound (Sound.Explosion, SoundManager.i.volume);
			Spawner.i.SpawnObject (Prefab.Explosion, gameObject.transform.position);
		    if (other.gameObject.GetComponent<PlayerController>() != null)
		    {
		        other.gameObject.GetComponent<PlayerController>().DamageVisual();
		    }
		    else
		    {
                other.gameObject.GetComponent<FusedPlayer>().DamageVisual();
            }
			Destroy(gameObject);
		}
	}
}

/// <summary>
/// Defines enemy attack pattern
/// </summary>
public enum EnemyAttackType{
	StandardSingle = 0,
	DirectedSingle = 1,
	RandomSingle = 2,
	CircleEight = 3,
	CardinalFour = 4,
	NoncardinalFour = 5,
	Spiral = 6,
    CircleThirtyTwo = 7,
    CircleSixtyFour = 8,
	SpiralWiggle = 9,
	SpiralWiggleSixteen = 10,
	CircleFourRandom = 11,
	CircleEightRandom = 12,
	CircleSixteenRandom = 13
};