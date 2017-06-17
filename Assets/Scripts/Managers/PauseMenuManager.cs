using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*********************************************************************************
 * class PauseMenuManager
 * 
 * Function: Controls all pause menu functionality
 *********************************************************************************/
public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager i;   //Static reference to PauseMenuManager
    public List<GameObject> buttons;    //List of all buttons on the pause menu
    public GameObject currentButton;    //Currently selected button
    int currentChoice;                  //Index of currently selected button
    bool inputReceived;                 //Determines if the user has given input. Prevents
                                        //input events from firing rapidly

    //Get static reference and pause the game
    void Awake()
	{
	    if (PauseMenuManager.i == null)
	    {
            i = this;
	    }

        //Pause the game
	    Time.timeScale = 0;
		if (ScreenShake.i != null) {
			ScreenShake.i.EndShake ();
		}
	}

    //Set the initially selected button
    void Start()
    {
        currentChoice = 0;
        currentButton = buttons[currentChoice];
        currentButton.GetComponent<Image>().color = Color.green;
    }

    //Check for user input
    void Update()
    {
        GetInput();
    }

    /// <summary>
    /// Handle user input. Can either select a new currently selected button or trigger an event based
    /// on the currently selected button
    /// </summary>
    void GetInput()
    {
        
        //Handle confirm input
        if (Input.GetButtonDown("A1") || Input.GetButtonDown("A2") || Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            CheckChoice();
        }

        //Handle movement input
        if ((Input.GetAxis("LSY1") > .2f || Input.GetAxis("LSY2") > .2f || Input.GetKeyDown(KeyCode.S)))
        {
            if (!inputReceived)
            {
                currentChoice++;
                if (currentChoice > buttons.Count - 1)
                {
                    currentChoice = 0;
                }
                ChangeSelectedButtonVisual(buttons[currentChoice]);
                inputReceived = true;
            }
        }
        else if ((Input.GetAxis("LSY1") < -.2f || Input.GetAxis("LSY2") < -.2f || Input.GetKeyDown(KeyCode.W)))
        {
            if (!inputReceived)
            {
                currentChoice--;
                if (currentChoice < 0)
                {
                    currentChoice = buttons.Count - 1;
                }
                ChangeSelectedButtonVisual(buttons[currentChoice]);
                inputReceived = true;
            }
        }
        else
        {
            inputReceived = false;
        }
        
    }

    /// <summary>
    /// Changes the visual representation of the currently selected button
    /// </summary>
    /// <param name="newButton"></param>
    private void ChangeSelectedButtonVisual(GameObject newButton)
    {
        currentButton.GetComponent<Image>().color = Color.white;
        newButton.GetComponent<Image>().color = Color.green;
        currentButton = newButton;
    }

    /// <summary>
    /// Triggers an event based on the currently selected button and the type of pause menu.
    /// </summary>
    void CheckChoice()
    {
        switch (buttons.Count)
        {
            case 1: /*Return to Main Menu is the only available option*/
                ToMainMenu();
                break;
            case 2: /*Load next scene, return to Main Menu. Default to Resume game*/
                switch (currentChoice)
                {
                    case 0:
                        Next();
                        break;
                    case 1:
                        ToMainMenu();
                        break;
                    default:
                        Resume();
                        break;
                }
                break;
            case 3: /*Resume game, Restart level, or return to main menu. Default to resume game*/
                switch (currentChoice)
                {
                    case 0:
                        Resume();
                        break;
                    case 1:
                        Restart();
                        break;
                    case 2:
                        ToMainMenu();
                        break;
                    default:
                        Resume();
                        break;
                }
                break;
            default: /*Defaults to resume game*/
                Resume();
                break;
        }
    }

    /// <summary>
    /// Return time scale to normal and destroy the pause menu
    /// </summary>
    public void Resume(){
		Time.timeScale = 1;
		Destroy (gameObject);
	}

    /// <summary>
    /// Restart the active scene
    /// </summary>
	public void Restart(){
		Time.timeScale = 1;
	    SoundManager.i.EndAllSound();
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

    /// <summary>
    /// Load the next level's story scene
    /// </summary>
    public void Next()
    {
        Time.timeScale = 1;
        SoundManager.i.EndAllSound();
        GameManager.i.level += 1;
        Debug.Log("Loading Level" + GameManager.i.level.ToString() + "Intro");
        SceneManager.LoadScene("Level" + GameManager.i.level.ToString() + "Intro");
    }

    /// <summary>
    /// Return to main menu
    /// </summary>
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SoundManager.i.EndAllSound();
        GameManager.i.level = 1;
        GameManager.i.score = 0;
        SceneManager.LoadScene("main_menu");
    }

    /// <summary>
    /// Quit the application
    /// </summary>
	public void Quit(){
		Application.Quit ();
	}
}
