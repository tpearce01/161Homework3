using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************************************************************
 * class HUDManager
 * 
 * Function: Controls the HUD overlay and all associated functionality
 *********************************************************************************/
public class HUDManager : MonoBehaviour
{
    public static HUDManager i;     //HUDManager reference
    public Text score;              //Player score
    public List<Image> p1Lives;     //Player life images
    private int lastScore;

    /// <summary>
    /// Get static reference and reset lastScore
    /// </summary>
    void Awake()
    {
        i = this;
        lastScore = 0;
    }

    /// <summary>
    /// Display life images and score text
    /// </summary>
    void Start()
    {
        UpdateLives(1);
        UpdateScore();
    }

    /// <summary>
    /// Update the number of lives to display
    /// </summary>
    /// <param name="player"></param>
    public void UpdateLives(int player)
    {
        if (player == 1)
        {
            switch (Level.i.lives)
            {
                case 3:
                    p1Lives[2].enabled = false;
                    break;
                case 2:
                    p1Lives[2].enabled = false;
                    p1Lives[1].enabled = false;
                    break;
                case 1:
                    p1Lives[2].enabled = false;
                    p1Lives[1].enabled = false;
                    p1Lives[0].enabled = false;
                    break;
                case 0:
                    p1Lives[2].enabled = false;
                    p1Lives[1].enabled = false;
                    p1Lives[0].enabled = false;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Update the score text to display
    /// </summary>
    public void UpdateScore()
    {
        bool ready = GameManager.i.GetReady();
        score.text = "Score: " + Level.i.playerScore;
    }
}
