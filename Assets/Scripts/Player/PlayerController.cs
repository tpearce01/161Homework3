using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class PlayerController
 * 
 * Function: Handles player input pertaining to controlling their ship in game.
 *********************************************************************************/
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;      //Rigidbody
    public int moveSpeed;               //Speed of movement
    private AttackType at;              //Current attack type
    public float TimeBetweenAttacks;    //Time between attacks
    private float attackCooldown;       //Remaining time until the player may attack again
    public Unit stats;                  //Reference to unit script attached to player
	public int controllerNumber;		//Controller number associated with this player
    private Vector3 finalPos;           //Final position to move player to
	public bool readyToFuse;

    /// <summary>
    /// Initialize default values and add player to list of players
    /// </summary>
    void Start()
    {
        Initialize();
		GameManager.i.AddPlayer (gameObject);
    }

    // Check for shoot input
    void Update()
    {
        Shoot();
		//Fusion ();
    }

    //Move the player
    void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// Set up default values
    /// </summary>
    void Initialize()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        at = AttackType.Standard;
        gameObject.GetComponent<Unit>().health = /*Level.i.totalHealth / 2*/ 100;
        gameObject.GetComponent<Unit>().ModifyHealth(0);
        gameObject.GetComponent<Unit>().SetImmortal(2);
        StartCoroutine(ImmortalVisualRoutine(2));
    }

    /// <summary>
    /// DEPRECATED FUNCTION. INTENDED FOR MULTIPLAYER VERSION
    /// </summary>
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

    /// <summary>
    /// Check for and handle shoot input
    /// </summary>
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

    /// <summary>
    /// Determines what the primary weapon does
    /// </summary>
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

    /// <summary>
    /// Standard attack. Straight and forward.
    /// </summary>
    public void AttackType0()
    {
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
        Spawner.i.SpawnPlayerBullet(gameObject.transform.position + Vector3.right + Vector3.up, controllerNumber);
        Spawner.i.SpawnPlayerBullet(gameObject.transform.position + Vector3.right - Vector3.up, controllerNumber);
    }

    /// <summary>
    /// Handle player movement input
    /// </summary>
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

    //Remove the player from the list of players when it is destroyed
	void OnDestroy(){
		GameManager.i.RemovePlayer (gameObject);
	}

    /// <summary>
    /// Gets keyboard input for movement and sets desired position
    /// </summary>
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

    /// <summary>
    /// Gets controller input for movement and sets desired position
    /// </summary>
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

    /// <summary>
    /// Display visual flash. Intended to display when damaged
    /// </summary>
    public void DamageVisual()
    {
        StartCoroutine(DamageVisualRoutine());
    }

    /// <summary>
    /// Display visual flash. Intended to display when damaged
    /// </summary>
    /// <returns></returns>
    public IEnumerator DamageVisualRoutine()
    {
        bool isRed = false;
        SpriteRenderer sr = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        for (int j = 0; j < 5; j++)
        {
            if (isRed)
            {
                sr.color = Color.gray;
                isRed = false;
            }
            else
            {
                sr.color = Color.red;
                isRed = true;
            }
            yield return new WaitForSeconds(.05f);
        }
        sr.color = Color.white;
        yield return null;
    }

    /// <summary>
    /// Display visual flash. Intended to display when immortal
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator ImmortalVisualRoutine(float duration)
    {
        bool isRed = false;
        SpriteRenderer sr = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        for (int j = 0; j < duration/.05f; j++)
        {
            if (isRed)
            {
                sr.color = Color.white;
                isRed = false;
            }
            else
            {
                sr.color = Color.clear;
                isRed = true;
            }
            yield return new WaitForSeconds(.05f);
        }
        sr.color = Color.white;
        yield return null;
    }
}

//Enum to improve readability for attack types
public enum AttackType
{
    Standard = 0
};
