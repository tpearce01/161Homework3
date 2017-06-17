using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class FusedPlayer
 * 
 * Function: DEPRECATED CLASS. This class is meant for multiplayer functionality
 *      which is not present in this version. Controls fused ship inputs
 *********************************************************************************/
public class FusedPlayer : MonoBehaviour {
	public static FusedPlayer i;        //Public reference to the fused player
	private Rigidbody2D rigidbody;      //Rigidbody
	public int moveSpeed;               //Speed of movement
	private AttackType p1at;            //Current attack type for player 1
	private AttackType p2at;			//Current attack type for player 2
	public float TimeBetweenAttacks;    //Time between attacks
	private float attackCooldownP1;     //Remaining time until player 1 may attack again
	private float attackCooldownP2;		//Remaining time until player 2 can attack again
	public Unit stats;                  //Reference to unit script attached to player
	protected int controllerNumber;		//Controller number associated with this player
	private Vector3 finalPos;           //Final position to move player to

    //Get static reference
	void Awake()
	{
		i = this;
	}

    //Add the fused player to list of players and initialize default values
	void Start()
	{
		Initialize();
		GameManager.i.AddPlayer (gameObject);
	}

    //Check for shoot input
	void Update()
	{
		Shoot();
	}

    //Check for movement input
	void FixedUpdate()
	{
		MovePlayer();
	}

	/// <summary>
    /// Set default values
    /// </summary>
	void Initialize()
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		p1at = AttackType.Standard;
		p2at = AttackType.Standard;
        gameObject.GetComponent<Unit>().damageModifier = 0.25f;
        //gameObject.GetComponent<Unit>().health = Level.i.fusedHealth;
        gameObject.GetComponent<Unit>().ModifyHealth(0);
    }

	/// <summary>
    /// Check for shoot input and fire bullet(s) if neccessary
    /// </summary>
	public void Shoot()
	{
		//Check for Player 1 Attack Input
		if (attackCooldownP1 <= 0)
		{
			if (/*KEYBOARD INPUT FOR TESTING ONLY*/Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)	//Keyboard input	
				||	Input.GetButton("A1"))					//Controller input
			{
				Weapon(p1at, 1);
				attackCooldownP1 = TimeBetweenAttacks;
			}
		}
		else
		{
			attackCooldownP1 -= Time.deltaTime;
		}

		//Check for Player 2 Attack Input
		if (attackCooldownP2 <= 0)
		{
			if (/*KEYBOARD INPUT FOR TESTING ONLY*/Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)	//Keyboard input	
				||	Input.GetButton("A2"))					//Controller input
			{
				Weapon(p2at, 2);
				attackCooldownP2 = TimeBetweenAttacks;
			}
		}
		else
		{
			attackCooldownP2 -= Time.deltaTime;
		}

		//Single-use Secondary attack
		if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Mouse1)	//Keyboard input
			||	Input.GetButtonDown("B1") || Input.GetButtonDown("B2"))								//Controller input
		{
			//Secondary Weapon
		}
	}

	/// <summary>
    /// Determines player attack type
    /// </summary>
    /// <param name="at"></param>
    /// <param name="player"></param>
	public void Weapon(AttackType at, int player)
	{
		switch((int)at)
		{
		case 0:
			AttackType0(player);
			break;
		default:
			AttackType0(player);
			break;
		}
	}

	/// <summary>
    /// Standard attack. Straight and forward.
    /// </summary>
    /// <param name="player"></param>
	public void AttackType0(int player)
	{
		//SoundManager.i.PlaySound (Sound.Shot1, 0.5f);
		Spawner.i.SpawnPlayerBullet(gameObject.transform.position + Vector3.right + Vector3.up, player);
		Spawner.i.SpawnPlayerBullet(gameObject.transform.position + Vector3.right - Vector3.up, player);
	}

	/// <summary>
    /// Get movement input and move the player if applicable
    /// </summary>
	public void MovePlayer()
	{
		finalPos = gameObject.transform.position;
		ControllerInput();
		// KEYBOARD CONTROLS FOR TESTING
		if (finalPos == gameObject.transform.position)
		{
			KeyboardInput();
		}
		// END TEST CODE
		rigidbody.MovePosition(Vector2.MoveTowards(gameObject.transform.position, finalPos, moveSpeed*Time.deltaTime));
	}

    //Remove the player from the list of players when it is destroyed
	void OnDestroy(){
        GameManager.i.RemovePlayer (gameObject);
	}

	//KEYBOARD CONTROLS FOR TESTING ONLY
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
    /// Get user input
    /// </summary>
	void ControllerInput()
	{
		Vector2 ls1 = new Vector2(Input.GetAxis("LSX1"), Input.GetAxis("LSY1"));
		Vector2 ls2 = new Vector2 (Input.GetAxis ("LSX2"), Input.GetAxis ("LSY2"));
		if (ls1.y > .2f && ls2.y > .2f)
		{
			finalPos -= transform.up;
		}
		if (ls1.y < -.2f && ls2.y < -.2f)
		{
			finalPos += transform.up;
		}
		if (ls1.x < -.2f && ls2.x < -.2f)
		{
			finalPos -= transform.right;
		}
		if (ls1.x > .2f && ls2.x > .2f)
		{
			finalPos += transform.right;
		}
	}

    /// <summary>
    /// Display visual flashing effect. Meant to occur when damaged.
    /// </summary>
    public void DamageVisual()
    {
        StartCoroutine(DamageVisualRoutine());
    }

    /// <summary>
    /// Display visual flashing effect. Meant to occur when damaged.
    /// </summary>
    /// <returns></returns>
    public IEnumerator DamageVisualRoutine()
    {
        Debug.Log("Coroutine start");
        bool isRed = false;
        SpriteRenderer sr = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        for (int j = 0; j < 5; j++)
        {
            Debug.Log("Inside coroutine");
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
        Debug.Log("Exiting coroutine");
        sr.color = Color.white;
        yield return null;
    }

}

