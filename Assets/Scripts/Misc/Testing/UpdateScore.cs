using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateScore : MonoBehaviour {

    private LevelByPhase levelByPhase;
    private Text text;
    public int player;
	// Use this for initialization
    void Awake()
    {
        this.text = this.GetComponent<Text>();
        this.levelByPhase = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<LevelByPhase>();
    }
	void Start ()
    {
        text.text += " "+levelByPhase.playerScore[player].ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
