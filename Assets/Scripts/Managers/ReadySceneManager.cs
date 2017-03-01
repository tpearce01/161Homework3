using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadySceneManager : MonoBehaviour
{
    public BreatheAnimation ba;
    public Text[] t;
	public string sceneToLoad;
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Start1"))
	    {
	        POneReady();
	    }
	    if (Input.GetButtonDown("Start2"))
	    {
	        PTwoReady();
	    }
	    if (Input.GetButtonDown("B1"))
	    {
	        POneNotReady();
	    }
	    if (Input.GetButtonDown("B2"))
	    {
	        PTwoNotReady();
	    }
	    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
	    {
            POneReady();
        }
	}

    public void POneReady()
    {
        CheckStart();
        GameManager.i.ReadyUp(0);
        ba.RemoveObj(t[0].gameObject);
        t[0].text = "Player 1 Ready!";
        t[0].color = Color.black;
    }

    public void PTwoReady()
    {
        CheckStart();
        GameManager.i.ReadyUp(1);
        ba.RemoveObj(t[1].gameObject);
        t[1].text = "Player 2 Ready!";
        t[1].color = Color.black;
    }

    public void POneNotReady()
    {
        GameManager.i.NotReady(0);
        ba.AddObj(t[0].gameObject);
        t[0].text = "Player 1 Press Start";
        t[0].color = Color.red;
    }

    public void PTwoNotReady()
    {
        GameManager.i.NotReady(1);
        ba.AddObj(t[1].gameObject);
        t[1].text = "Player 2 Press Start";
        t[1].color = Color.red;
    }

    public void CheckStart()
    {
        foreach (bool b in GameManager.i.GetReady())
        {
            if (b)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
