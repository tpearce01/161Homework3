using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*********************************************************************************
 * class StorySceneManager
 * 
 * Function: Handles all Story scene functionalities
 *********************************************************************************/
public class StorySceneManager : MonoBehaviour
{
    public Sprite background;                                   //Background image
    public Sound backgroundMusic;                               //Background music

    public StoryCharacter[] characters = new StoryCharacter[2]; //Characters in this story scene
    public List<Chat> conversation = new List<Chat>();          //Chat log of the characters conversation
    private int currentChat;                                    //Current index of the chat log (conversation)
    private Text storyText;                                     //Currently displayed text

    private Image leftSpeaker;                                  //Left character image
    private Image rightSpeaker;                                 //Right character image

    public string sceneToLoad;                                  //Next scene to load

    //Set character images, background, and music
    void Start()
    {
        leftSpeaker = gameObject.transform.Find("LeftSpeaker").GetComponent<Image>();
        leftSpeaker.sprite = characters[0].characterImage;
        leftSpeaker.preserveAspect = true;
        rightSpeaker = gameObject.transform.Find("RightSpeaker").GetComponent<Image>();
        rightSpeaker.sprite = characters[1].characterImage;
        rightSpeaker.preserveAspect = true;
        gameObject.transform.Find("Background").GetComponent<Image>().sprite = background;
        storyText = gameObject.transform.Find("StoryText").GetComponent<Text>();
        SoundManager.i.PlaySoundLoop(backgroundMusic, SoundManager.i.volume);
        NextChat();
    }

    //Check for user input for the next chat log
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)
            || Input.GetButtonDown("A1") || Input.GetButtonDown("A2")
            || Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2"))
        {
            NextChat();
        }

    }

    /// <summary>
    /// Display the next chat log and highlight the speaking character
    /// </summary>
    void NextChat()
    {
        if (currentChat >= conversation.Count)
        {
            NextScene();
            return;
        }

        //Set character colors
        if (conversation[currentChat].characterSpeaking == 0)
        {
            leftSpeaker.color = Color.white;
            rightSpeaker.color = Color.gray;
        }
        else /*conversation[currentChat].characterSpeaking == 1*/
        {
            leftSpeaker.color = Color.gray;
            rightSpeaker.color = Color.white;
        }

        //Update story text
        storyText.text = characters[conversation[currentChat].characterSpeaking].name + ": " + conversation[currentChat].text;

        currentChat++;
    }

    /// <summary>
    /// Load the next scene
    /// </summary>
    void NextScene()
    {
        SoundManager.i.EndAllSound();
        SceneManager.LoadScene(sceneToLoad);
    }
}
