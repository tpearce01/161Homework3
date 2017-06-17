using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*********************************************************************************
 * class EndSceneManager
 * 
 * Function: Plays the final cutscene to end the game
 *********************************************************************************/
public class EndSceneManager : MonoBehaviour {

	public GameObject p1;       //Player object reference
	bool p1Warp;                //Determines if the player has engaged fast travel
	public float timer1;        //Time until fast travel
	public int speedSlow;       //Inital speed
	public int speedFast;       //Fast travel speed
	public Image FadeImage;     //Black panel to fade
	bool fading;                //Determines if the game is currently fading out
	public GameObject endPanel; //End of game panel

	// Ensure endPanel is not active and the fade image is transparent
	void Start () {
		endPanel.SetActive (false);
		FadeImage.color = new Color(0,0,0,0);
	}
	
	// Controls player movement, displaying the end panel, and beginning fade out
	void Update () {
		timer1 += Time.deltaTime;

		if (!p1Warp && timer1 > -1.5) {
			SoundManager.i.PlaySound (Sound.WarpSpeed, SoundManager.i.volume/4);
			p1Warp = true;
		}

		//Move p1
		if (timer1 <= 1) {
			Move (p1, speedSlow);
		} else {
			Move (p1, speedFast);
		}	

        //Display end panel
		if (timer1 > 4 && !fading) {
			endPanel.SetActive (true);
			fading = true;
		}

        //Start fading
		if (timer1 > 3 && !fading) {
			StartCoroutine (Fade ());
			fading = true;
		}
	}

    /// <summary>
    /// Move the object at the specified speed
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="speed"></param>
	void Move(GameObject obj, float speed){
		obj.transform.position += transform.right * speed * Time.deltaTime;
	}

    /// <summary>
    /// Fade to black at a rate of 1% every .025s
    /// </summary>
    /// <returns></returns>
	IEnumerator Fade(){
		float alpha = 0;
		for (int i = 0; i < 100; i++) {
			FadeImage.color = new Color (0,0,0,alpha);
			alpha += .01f;
			yield return new WaitForSeconds (0.025f);
		}

		fading = false;
		endPanel.SetActive (true);

		yield return null;
	}

    /// <summary>
    /// return to main menu
    /// </summary>
	public void ReturnToMenu(){
		SceneManager.LoadScene ("main_menu");
	}

}
