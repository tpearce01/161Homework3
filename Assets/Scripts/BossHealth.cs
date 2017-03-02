﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    // Use this for initialization
    private  float bossHealthMax;
    private float bossHealthCurrent;

    public Slider healthSlider;
    public Image healthImage;  
    
    void Awake()
    {
        bossHealthMax = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss0>().bossMaxHealth;
    }
	void Start ()
    {
        bossHealthCurrent = bossHealthMax;
        modifyBossHealth(); 
	}
	
	// Update is called once per frame
	void Update ()
    {

        bossHealthCurrent = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss0>().bossCurrentHealth;
        modifyBossHealth();

        print(bossHealthCurrent);
    }

    public void modifyBossHealth()
    {
        if (bossHealthCurrent >= bossHealthMax)
        {
            bossHealthCurrent = bossHealthMax;
        }

        healthSlider.value = bossHealthCurrent / bossHealthMax;

        //Color-Based health feedback
        if (healthSlider.value > .50f)
        {
            healthImage.color = Color.Lerp(Color.yellow, Color.green, (healthSlider.value * 2) - 1);
        }
        else if (healthSlider.value < 0.5f)
        {
            healthImage.color = Color.Lerp(Color.red, Color.yellow, healthSlider.value * 2);
        }

        //If dead, kill the unit
        if (bossHealthCurrent <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(GameObject.FindGameObjectWithTag("Boss"));
    }
    

}


