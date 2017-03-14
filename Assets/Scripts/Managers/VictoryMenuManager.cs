using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMenuManager : PauseMenuManager
{
    public Text[] scoreText = new Text[2];

    void Start()
    {
        bool[] ready = GameManager.i.GetReady();

        for (int j = 0; j < GameManager.i.score.Length; j++)
        {
            GameManager.i.score[j] += Level.i.playerScore[j];
        }

        for (int j = 0; j < ready.Length; j++)
        {
            if (ready[j])
            {
                scoreText[j].text = "Player " + (j + 1) + " Score: " + GameManager.i.score[j];
            }
            else
            {
                scoreText[j].text = "";
            }
        }

        
    }
}
