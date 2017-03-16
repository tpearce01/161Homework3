using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScene : MonoBehaviour {

	public string sceneToLoad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			LoadScene (sceneToLoad);
		}
	}

	void LoadScene(string scene){
		SoundManager.i.EndAllSound ();
		SceneManager.LoadScene (scene);
	}
}
