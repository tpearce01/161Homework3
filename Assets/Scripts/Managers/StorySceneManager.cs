using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySceneManager : MonoBehaviour
{
    public Sprite background;
    public Sound backgroundMusic;

    public StoryCharacter[] characters = new StoryCharacter[2];
    public List<Chat> conversation = new List<Chat>();
    private int currentChat;
    private Text storyText;

    private Image leftSpeaker;
    private Image rightSpeaker;

    public string sceneToLoad;

    void Start()
    {
        leftSpeaker = gameObject.transform.FindChild("LeftSpeaker").GetComponent<Image>();
        leftSpeaker.sprite = characters[0].characterImage;
        leftSpeaker.preserveAspect = true;
        rightSpeaker = gameObject.transform.FindChild("RightSpeaker").GetComponent<Image>();
        rightSpeaker.sprite = characters[1].characterImage;
        rightSpeaker.preserveAspect = true;
        gameObject.transform.FindChild("Background").GetComponent<Image>().sprite = background;
        storyText = gameObject.transform.FindChild("StoryText").GetComponent<Text>();
        SoundManager.i.PlaySound(backgroundMusic, SoundManager.i.volume);
        NextChat();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)
            || Input.GetButtonDown("A1") || Input.GetButtonDown("A2")
            || Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2"))
        {
            NextChat();
        }

    }

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

    void NextScene()
    {
        SoundManager.i.EndSoundAbrupt("StoryAudio1");
        SceneManager.LoadScene(sceneToLoad);
    }
}
