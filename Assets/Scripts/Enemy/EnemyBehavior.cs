using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody2D rigidbody;
	public EnemyAttackType attackType;
    public float timeBetweenAttacks;
    public float attackCooldown;
    public int speed;
	private int spiralDegree;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate ()
	{
	    Move();				
	}

	void Update(){
		Shoot ();
	}

	//Move straight toward the left side of the screen
    void Move()
    {
        rigidbody.transform.position -= transform.right*speed*Time.deltaTime;
    }

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
			default:
				StandardSingle ();
				break;
			}
		}
	}

	//Single bullet forward
	void StandardSingle()
	{
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnObject (Prefab.Shot2, gameObject.transform.position);
	}

	//Single bullet towards the player
	void DirectedSingle(){
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnObjectTowardRandomPlayer (Prefab.Shot2, gameObject.transform.position);
	}

	//Spawns 2 bullets forward
	void StandardDouble(){
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnObject (Prefab.Shot2, gameObject.transform.position + transform.up);
		Spawner.i.SpawnObject (Prefab.Shot2, gameObject.transform.position - transform.up);
	}

	//Spawns 1 bullet in a random direction
	void RandomSingle(){
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnObjectWithRotation (Prefab.Shot2, gameObject.transform.position, new Vector3(0,0,Random.Range(-180,181)));
	}

	//Spawns a circle that shoots in the cardinal directions
	void CircleDynamic(int bullets){
		for (int i = 0; i < bullets; i++) {
			Spawner.i.SpawnObjectWithRotation(Prefab.Shot2, gameObject.transform.position, new Vector3(0,0,i * 360 / bullets));
		}
	}

	//Spawns a circle that does not shoot in the cardinal directions. Offset by half the angle between shots
	void OffsetCircleDynamic(int bullets){
		for (int i = 0; i < bullets; i++) {
			Spawner.i.SpawnObjectWithRotation(Prefab.Shot2, gameObject.transform.position, new Vector3(0,0,i * 360 / bullets + 360 / bullets / 2));
		}
	}

	//Rapid fire in a spiral pattern
	void Spiral(){
		Spawner.i.SpawnObjectWithRotation(Prefab.Shot2, gameObject.transform.position, new Vector3(0,0,spiralDegree));
		spiralDegree += 15;
	}

	//If the unit collides with the player, destroy it
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("Player")){
			other.gameObject.GetComponent<Unit>().ModifyHealth(-20);
			//SoundManager.i.PlaySound (Prefab.Explosion1, 0.5f);
			Spawner.i.SpawnObject (Prefab.Explosion, gameObject.transform.position);
			Destroy(gameObject);
		}
	}
}

//Defines the type of attack
public enum EnemyAttackType{
	StandardSingle = 0,
	DirectedSingle = 1,
	RandomSingle = 2,
	CircleEight = 3,
	CardinalFour = 4,
	NoncardinalFour = 5,
	Spiral = 6,
    CircleThirtyTwo = 7,
    CircleSixtyFour = 8
};