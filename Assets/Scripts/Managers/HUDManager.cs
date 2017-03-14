using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager i;
    public Text[] scores = new Text[2];
    public List<Image> p1Lives;
    public List<Image> p2Lives;
    private int[] lastScore;

    void Awake()
    {
        i = this;
        lastScore = new int[2];
    }

    void Start()
    {
        UpdateLives(1);
        UpdateLives(2);
        UpdateScore();
    }

    public void UpdateLives(int player)
    {
        if (player == 1)
        {
            switch (Level.i.lives[player - 1])
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
        else
        {
            switch (Level.i.lives[player - 1])
            {
                case 3:
                    p2Lives[2].enabled = false;
                    break;
                case 2:
                    p2Lives[2].enabled = false;
                    p2Lives[1].enabled = false;
                    break;
                case 1:
                    p2Lives[2].enabled = false;
                    p2Lives[1].enabled = false;
                    p2Lives[0].enabled = false;
                    break;
                case 0:
                    p2Lives[2].enabled = false;
                    p2Lives[1].enabled = false;
                    p2Lives[0].enabled = false;
                    break;
                default:
                    break;
            }
        }
    }

    public void UpdateScore()
    {
        bool[] ready = GameManager.i.GetReady();
        for (int j = 0; j < ready.Length; j++)
        {
            if (ready[j])
            {
                scores[j].text = "Score: " + Level.i.playerScore[j];
            }
            else
            {
                scores[j].text = "";
            }
        }
    }
}
