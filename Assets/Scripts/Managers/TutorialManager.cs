using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public Text tutorialText;
	float timer;
	int phase;

	void Awake(){
		GameManager.i.ReadyUp (0);
		GameManager.i.ReadyUp (1);
		SoundManager.i.EndAllSound ();
	}

	// Use this for initialization
	void Start () {
		tutorialText.text = "Use the Left Stick to move.";
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 6) {
			UpdateText ();
			phase++;
			timer = 0;
		}

		if (GameObject.Find ("VictoryMenu(Clone)")) {
			Destroy (GameObject.Find ("VictoryMenu(Clone)"));
			Spawner.i.SpawnObject (Prefab.TutorialCompleteMenu, Vector3.zero);
		}
	}

	void UpdateText(){
		switch (phase) {
		case 0:
			tutorialText.text = "Hold A to shoot.";
			break;
		case 1:
			tutorialText.text = "Press Y to fuse with another player. You must be touching the other player to fuse. " +
				"The fused ship is extremely powerful, but requires pilots to move in unison. It does not share " +
				"health with individual ships.";
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
