using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************************************************************
 * class VictoryMenuManager : PauseMenuManager
 * 
 * Function: Displays victory stats. Child of PauseMenuManager
 *********************************************************************************/
public class VictoryMenuManager : PauseMenuManager
{
    public Text[] scoreText = new Text[2];  //Text objects to write to

    //Display player scores. Reduced functionality to 1 player, resulting in odd code style
    void Start()
    {
        bool ready = GameManager.i.GetReady();
        GameManager.i.score += Level.i.playerScore;

        if (ready)
        {
            scoreText[0].text = "Player " + (1) + " Score: " + GameManager.i.score;
        }
        else
        {
            scoreText[0].text = "";
        }

        
    }
}
