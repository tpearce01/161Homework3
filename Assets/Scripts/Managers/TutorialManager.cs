using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************************************************************
 * class TutorialManager
 * 
 * Function: Displays all tutorial text
 *********************************************************************************/
public class TutorialManager : MonoBehaviour {

	public Text tutorialText;   //Currently displayed tutorial text
	float timer;                //Time until next tutorial message
	int phase;                  //Current phase

    //Ensure all sound is off and set player status to ready
	void Awake(){
		GameManager.i.ReadyUp ();
		SoundManager.i.EndAllSound ();
	}

	// Display initial tutorial text
	void Start () {
		tutorialText.text = "Use the Left Stick to move.";
	}
	
	// Check for next phase, and display tutorial complete menu if appropriate
	void Update () {
		timer += Time.deltaTime;
		if (timer > 6) {
			UpdateText ();
			phase++;
			timer = 0;
		}

        //If victory menu is displayed, instead show the tutorial complete menu
		if (GameObject.Find ("VictoryMenu(Clone)")) {
			Destroy (GameObject.Find ("VictoryMenu(Clone)"));
			Spawner.i.SpawnObject (Prefab.TutorialCompleteMenu, Vector3.zero);
		}
	}

    //Display a set piece of text based on the current phase
	void UpdateText(){
		switch (phase) {
		case 0:
			tutorialText.text = "Hold A to shoot.";
			break;
		case 1:
			tutorialText.text = "Multiplayer controls are disabled.";
			break;
		case 2:
			tutorialText.text = "Lives refill at the beginning of each level.";
			break;
		case 3:
			tutorialText.text = "Defeat all enemies in a level to reach the boss.";
			break;
		case 4:
			tutorialText.text = "Attackable boss components are designated by a health bar below the vulnerable spot. " +
			"Destroying all components will damage the boss' total health.";
			break;
		default:
			tutorialText.text = "Use the Left Stick to move.";
			phase = 0;
			break;
		}
	}
}
