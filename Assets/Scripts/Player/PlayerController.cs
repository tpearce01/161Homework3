using System.Collections;
using System.Collections.Generic;
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
    private Vector3 finalPos;           //Final position to move player to
	public bool readyToFuse;

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
		Fusion ();
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

	void Fusion(){
		if(Input.GetButtonDown("Y" + controllerNumber)/*TESTING*/  || Input.GetKeyDown(KeyCode.Y)/*TESTING*/ )
        {
            Debug.Log("Player " + controllerNumber + " is ready to fuse");
			readyToFuse = true;
		}
		if (Input.GetButtonUp ("Y" + controllerNumber))
        {
			readyToFuse = false;
		}
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
                SoundManager.i.PlaySound(Sound.Shoot, SoundManager.i.volume/4);
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
        Spawner.i.SpawnPlayerBullet(gameObject.transform.position + Vector3.right + Vector3.up, controllerNumber);
        Spawner.i.SpawnPlayerBullet(gameObject.transform.position + Vector3.right - Vector3.up, controllerNumber);
    }

    //Get player input and move player is applicable
    public void MovePlayer()
    {
        finalPos = gameObject.transform.position;
        ControllerInput();
        if (finalPos == gameObject.transform.position)
        {
            KeyboardInput();
        }
        rigidbody.MovePosition(Vector2.MoveTowards(gameObject.transform.position, finalPos, moveSpeed*Time.deltaTime));
    }

	void OnDestroy(){
		GameManager.i.RemovePlayer (gameObject);
	}

    void KeyboardInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            finalPos += transform.up;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            finalPos -= transform.right;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            finalPos -= transform.up;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            finalPos += transform.right;
        }
    }

    void ControllerInput()
    {
        Vector2 ls = new Vector2(Input.GetAxis("LSX" + controllerNumber), Input.GetAxis("LSY" + controllerNumber));
        if (ls.y > .2f)
        {
            finalPos -= transform.up;
        }
        if (ls.y < -.2f)
        {
            finalPos += transform.up;
        }
        if (ls.x < -.2f)
        {
            finalPos -= transform.right;
        }
        if (ls.x > .2f)
        {
            finalPos += transform.right;
        }
    }
}

//Improve readability for attack types
public enum AttackType
{
    Standard = 0
};
