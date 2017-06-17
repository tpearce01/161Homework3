using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*********************************************************************************
 * class ReadySceneManager
 * 
 * Function: Handles all functionalities of the Ready scene. The Ready scene 
 *      determines when the player should enter the game
 *********************************************************************************/
public class ReadySceneManager : MonoBehaviour
{
    public BreatheAnimation ba; //Allows for "breathing" animation to play on objects
    public Text[] t;            //Ready up text
	public string sceneToLoad;  //Next scene to load
	
	// Handles user input
	void Update () {
	    if (Input.GetButtonDown("Start1"))
	    {
	        POneReady();
	    }
	    if (Input.GetButtonDown("B1"))
	    {
	        POneNotReady();
	    }
	    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
	    {
            POneReady();
        }
	}

    /// <summary>
    /// Sets player one to ready
    /// </summary>
    public void POneReady()
    {
        CheckStart();
        GameManager.i.ReadyUp();
        ba.RemoveObj(t[0].gameObject);
        t[0].text = "Player 1 Ready!";
        t[0].color = Color.black;
    }

    /// <summary>
    /// Sets player one to not ready
    /// </summary>
    public void POneNotReady()
    {
        GameManager.i.NotReady(0);
        ba.AddObj(t[0].gameObject);
        t[0].text = "Player 1 Press Start";
        t[0].color = Color.red;
    }

    /// <summary>
    /// Check if the next scene should be loaded
    /// </summary>
    public void CheckStart()
    {
        if (GameManager.i.GetReady())
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
