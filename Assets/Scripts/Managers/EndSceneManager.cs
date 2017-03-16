using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour {

	public GameObject p1;
	public GameObject p2;

	bool p1Warp;
	bool p2Warp;

	public float timer1;
	public float timer2;

	public int speedSlow;
	public int speedFast;

	public Image FadeImage;
	bool fading;

	public GameObject endPanel;

	// Use this for initialization
	void Start () {
		endPanel.SetActive (false);
		FadeImage.color = new Color(0,0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		timer1 += Time.deltaTime;
		timer2 += Time.deltaTime;

		if (!p1Warp && timer1 > -1.5) {
			SoundManager.i.PlaySound (Sound.WarpSpeed, SoundManager.i.volume/4);
			p1Warp = true;
		}
		if (!p2Warp && timer2 > -1.5) {
			SoundManager.i.PlaySound (Sound.WarpSpeed, SoundManager.i.volume/4);
			p2Warp = true;
		}

		//Move p1
		if (timer1 <= 1) {
			Move (p1, speedSlow);
		} else {
			Move (p1, speedFast);
		}

		//Move p2
		if (timer2 <= 1) {
			Move (p2, speedSlow);

		} else {
			Move (p2, speedFast);
		}	

		if (timer1 > 4 && !fading) {
			endPanel.SetActive (true);
			fading = true;
		}

		if (timer1 > 3 && !fading) {
			StartCoroutine (Fade ());
			fading = true;
		}


	}

	void Move(GameObject obj, float speed){
		obj.transform.position += transform.right * speed * Time.deltaTime;
	}

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

	public void ReturnToMenu(){
		SceneManager.LoadScene ("main_menu");
	}

}
