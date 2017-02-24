using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public GameObject loadPanel;

	public void QuitButton(){
		Application.Quit();
	}

	public void LoadScene(string scene){
		if (loadPanel != null) {
			loadPanel.SetActive (true);
		}
		SceneManager.LoadScene (scene);
	}

}
