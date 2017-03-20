using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager i;

    public List<GameObject> buttons;
    public GameObject currentButton;
    int currentChoice;
    bool inputReceived;

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

    void Start()
    {
        currentButton = buttons[currentChoice];
        currentButton.GetComponent<Image>().color = Color.green;
    }
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetButtonDown("A1") || Input.GetButtonDown("A2") || Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            CheckChoice();
        }

        if ((Input.GetAxis("LSY1") > .2f || Input.GetAxis("LSY2") > .2f || Input.GetKeyDown(KeyCode.S)))
        {
            if (!inputReceived)
            {
                currentChoice++;
                if (currentChoice > buttons.Count - 1)
                {
                    currentChoice = 0;
                }
                ChangeSelectedButtonVisual(buttons[currentChoice]);
                inputReceived = true;
            }
        }
        else if ((Input.GetAxis("LSY1") < -.2f || Input.GetAxis("LSY2") < -.2f || Input.GetKeyDown(KeyCode.W)))
        {
            if (!inputReceived)
            {
                currentChoice--;
                if (currentChoice < 0)
                {
                    currentChoice = buttons.Count - 1;
                }
                ChangeSelectedButtonVisual(buttons[currentChoice]);
                inputReceived = true;
            }
        }
        else
        {
            inputReceived = false;
        }
    }

    private void ChangeSelectedButtonVisual(GameObject newButton)
    {
        currentButton.GetComponent<Image>().color = Color.white;
        newButton.GetComponent<Image>().color = Color.green;
        currentButton = newButton;
    }

    void CheckChoice()
    {
        Debug.Log(buttons.Count);
        switch (buttons.Count)
        {
            case 1:
                ToMainMenu();
                break;
            case 2:
                switch (currentChoice)
                {
                    case 0:
                        Next();
                        break;
                    case 1:
                        ToMainMenu();
                        break;
                    default:
                        Resume();
                        break;
                }
                break;
            case 3:
                switch (currentChoice)
                {
                    case 0:
                        Resume();
                        break;
                    case 1:
                        Restart();
                        break;
                    case 2:
                        ToMainMenu();
                        break;
                    default:
                        Resume();
                        break;
                }
                break;
            default:
                Resume();
                break;
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
