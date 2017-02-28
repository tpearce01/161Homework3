using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	void Awake(){
		Time.timeScale = 0;
		if (ScreenShake.i != null) {
			ScreenShake.i.EndShake ();
		}
	}

	public void Resume(){
		Time.timeScale = 1;
		Destroy (gameObject);
	}

	public void Restart(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void Quit(){
		Application.Quit ();
	}
}
