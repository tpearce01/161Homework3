using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager i;

    void Awake()
	{
	    if (PauseMenuManager.i == null)
	    {
            i = this;
	    }

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
	    SoundManager.i.EndAllSound();
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

    public void Next()
    {
        Time.timeScale = 1;
        SoundManager.i.EndAllSound();
        GameManager.i.level += 1;
        Debug.Log("Loading Level" + GameManager.i.level.ToString() + "Intro");
        SceneManager.LoadScene("Level" + GameManager.i.level.ToString() + "Intro");
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SoundManager.i.EndAllSound();
        GameManager.i.level = 1;
        GameManager.i.score = new int[2];
        SceneManager.LoadScene("main_menu");
    }

	public void Quit(){
		Application.Quit ();
	}
}
