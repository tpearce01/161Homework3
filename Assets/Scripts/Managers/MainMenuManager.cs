using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour {

	public GameObject loadPanel;
	public List<GameObject> buttons;
	public GameObject currentButton;
	public BreatheAnimation ba;

	int currentChoice;
	bool inputReceived;

	void Start(){
		SoundManager.i.PlaySoundLoop (Sound.MainMenuAudio, SoundManager.i.volume);
		ba.AddObj (buttons[currentChoice]);
		currentButton = buttons [currentChoice];
		currentButton.GetComponent<Image> ().color = Color.green;
	}

	void Update(){
		if (Input.GetButtonDown ("Start1") || Input.GetButtonDown ("Start2") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
			LoadScene ();
		}
		if ((Input.GetAxis ("LSX1") < -.2f || Input.GetKeyDown(KeyCode.S)) && !inputReceived) {
			currentChoice++;
			if (currentChoice > 2) {
				currentChoice = 0;
			}
			ChangeSelectedButtonVisual (buttons [currentChoice]);
			inputReceived = true;
		} else if ((Input.GetAxis ("LSX1") > .2f || Input.GetKeyDown(KeyCode.W)) && !inputReceived) {
			currentChoice--;
			if (currentChoice < 0) {
				currentChoice = 2;
			}
			ChangeSelectedButtonVisual (buttons [currentChoice]);
			inputReceived = true;
		} else {
			inputReceived = false;
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
