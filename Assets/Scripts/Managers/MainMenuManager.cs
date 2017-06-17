using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

/*********************************************************************************
 * class MainMenuManager
 * 
 * Function: Controls all main menu functionality
 *********************************************************************************/
public class MainMenuManager : MonoBehaviour {

	public GameObject loadPanel;        //Loading screen object
	public List<GameObject> buttons;    //List of all buttons in the scene
	public GameObject currentButton;    //Currently selected button
	public BreatheAnimation ba;         //Allows a "breathing" animation to play on objects
	int currentChoice;                  //Index of currently selected button
	bool inputReceived;                 //Determines if user has given input. Used to prevent 
                                        //input from firing events too quickly

    //Play main menu background music and set first button as currently selected button
	void Start(){
		SoundManager.i.PlaySoundLoop (Sound.MainMenuAudio, SoundManager.i.volume);
		ba.AddObj (buttons[currentChoice]);
		currentButton = buttons [currentChoice];
		currentButton.GetComponent<Image> ().color = Color.green;
	}

    /// <summary>
    /// Check for user input. Can either select a new button, or load a scene based on the currently selected button
    /// </summary>
	void Update(){
        //Handle confirm input
		if (Input.GetButtonDown ("Start1") || Input.GetButtonDown("Start2") || Input.GetButtonDown("A1") || Input.GetButtonDown("A2") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
			LoadScene ();
		}

        //Handle movement input
		if ((Input.GetAxis("LSY1") > .2f || Input.GetAxis("LSY2") > .2f || Input.GetKeyDown(KeyCode.S))) {
		    if (!inputReceived)
		    {
		        currentChoice++;
		        if (currentChoice > 2)
		        {
		            currentChoice = 0;
		        }
		        ChangeSelectedButtonVisual(buttons[currentChoice]);
		        inputReceived = true;
		    }
		} else if ((Input.GetAxis("LSY1") < -.2f || Input.GetAxis("LSY2") < -.2f || Input.GetKeyDown(KeyCode.W))) {
		    if (!inputReceived)
		    {
		        currentChoice--;
		        if (currentChoice < 0)
		        {
		            currentChoice = 2;
		        }
		        ChangeSelectedButtonVisual(buttons[currentChoice]);
		        inputReceived = true;
		    }
		} else {
			inputReceived = false;
		}
	}

    /// <summary>
    /// Quits the application
    /// </summary>
	public void QuitButton(){
		Application.Quit();
	}

    /// <summary>
    /// Loads the specified scene.
    /// </summary>
    /// <param name="scene"></param>
	public void LoadScene(string scene){
		if (loadPanel != null) {
			loadPanel.SetActive (true);
		}
		SceneManager.LoadScene (scene);
	}

    /// <summary>
    /// Determines which scene to load
    /// </summary>
	void LoadScene(){
		switch (currentChoice) {
		case 0:
			SceneManager.LoadScene ("ready_scene");
			break;
		case 1:
			SceneManager.LoadScene ("LevelTutorial");
			break;
		case 2:
			Application.Quit ();
			break;
		default:
			break;
		}
	}

    /// <summary>
    /// Changes the visual representation of the currently selected button
    /// </summary>
    /// <param name="newButton"></param>
	private void ChangeSelectedButtonVisual(GameObject newButton)
	{
		currentButton.GetComponent<Image> ().color = Color.white;
		newButton.GetComponent<Image> ().color = Color.green;
		ba.ResetObjScale ();
		ba.RemoveObj (currentButton);
		ba.AddObj (newButton);
		currentButton = newButton;
	}

}
