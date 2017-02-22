using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public GameObject loadPanel;

	public void StartGame(){
        loadPanel.SetActive(true);
		SceneManager.LoadScene ("level_1");
	}

	public void QuitButton(){
		Application.Quit();
	}

}
