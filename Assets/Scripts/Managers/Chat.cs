using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class Chat
 * 
 * Function: Store information about a single chat event for a story scene. 
 *      Contains the index of the speaking character and the text to output.
 *********************************************************************************/
[System.Serializable]
public class Chat
{
    public int characterSpeaking;   //Index of the speaking character
    public string text;             //Text to display for this chat
}
