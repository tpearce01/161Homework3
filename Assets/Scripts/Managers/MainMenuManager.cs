using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public GameObject loadPanel;

	void Start(){
		SoundManager.i.PlaySound (Sound.MainMenuAudio, SoundManager.i.volume);
	}

	void Update(){
		if (Input.GetButtonDown ("Start1") || Input.GetButtonDown ("Start2") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
			LoadScene ("ready_scene");
		}
	}

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
