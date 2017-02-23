using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController i;   //Public reference to player
    private Rigidbody2D rigidbody;      //Rigidbody
    public int moveSpeed;               //Speed of movement
    private AttackType at;              //Current attack type
    public float TimeBetweenAttacks;    //Time between attacks
    private float attackCooldown;       //Remaining time until the player may attack again
    public Unit stats;                  //Reference to unit script attached to player
	protected int controllerNumber;		//Controller number associated with this player

    void Awake()
    {
        i = this;
    }

    void Start()
    {
        Initialize();
		GameManager.i.AddPlayer (gameObject);
    }

    void Update()
    {
        Shoot();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    //Setup default values
    void Initialize()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        at = AttackType.Standard;
    }

    //Shoot
    public void Shoot()
    {
        //Rapid-fire primary attack
        if (attackCooldown <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)	//Keyboard input	
				||	Input.GetButton("A" + controllerNumber))					//Controller input
            {
                //Primary Weapon
                PrimaryWeapon();
                attackCooldown = TimeBetweenAttacks;
            }
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }

        //Single-use Secondary attack
		if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Mouse1)	//Keyboard input
			||	Input.GetButtonDown("B" + controllerNumber))								//Controller input
        {
            //Secondary Weapon
        }
    }

    //Determines what the primary weapon does
    public void PrimaryWeapon()
    {
        switch((int)at)
        {
            case 0:
                AttackType0();
                break;
            default:
                AttackType0();
                break;
        }
    }

    //Standard attack
    public void AttackType0()
    {
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
        Spawner.i.SpawnObject(Prefab.Shot1, gameObject.transform.position + Vector3.right + Vector3.up);
        Spawner.i.SpawnObject(Prefab.Shot1, gameObject.transform.position + Vector3.right - Vector3.up);
    }

    //Get player input and move player is applicable
    public void MovePlayer()
    {
        Vector3 finalPos = gameObject.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            finalPos += transform.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            finalPos -= transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            finalPos -= transform.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            finalPos += transform.right;
        }
        
        rigidbody.MovePosition(Vector2.MoveTowards(gameObject.transform.position, finalPos, moveSpeed*Time.deltaTime));
    }

	void OnDestroy(){
		GameManager.i.RemovePlayer (gameObject);
	}
}

//Improve readability for attack types
public enum AttackType
{
    Standard = 0
};
