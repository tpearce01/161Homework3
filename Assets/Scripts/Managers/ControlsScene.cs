using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*********************************************************************************
 * class ControlsScene
 * 
 * Function: This script is active in the "Controls Scene". The scene will display
 *      user input controls until any button is pressed. Then, it will load the 
 *      next scene.
 *********************************************************************************/
public class ControlsScene : MonoBehaviour {

	public string sceneToLoad;  //Next scene to load
	
	//Check for input to load the next scene
	void Update () {
		if (Input.anyKeyDown) {
			LoadScene (sceneToLoad);
		}
	}

    /// <summary>
    /// Load the desired scene
    /// </summary>
    /// <param name="scene"></param>
	void LoadScene(string scene){
		SoundManager.i.EndAllSound ();
		SceneManager.LoadScene (scene);
	}
}
