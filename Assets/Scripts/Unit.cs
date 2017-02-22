using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public Slider healthSlider;
    public Image healthSliderFill;
    public float maxHealth;
    private float health;

    void Start()
    {
        health = maxHealth;
        ModifyHealth(0);
    }

    //Modify unit health and update slider value / color
    public void ModifyHealth(float value)
    {
        health += value;
        healthSlider.value = health/maxHealth;

        //Color-Based health feedback
        if (healthSlider.value > .50f)
        {
            healthSliderFill.color = Color.Lerp(Color.yellow, Color.green, (healthSlider.value * 2) - 1);
        }
        else if (healthSlider.value < 0.5f)
        {
            healthSliderFill.color = Color.Lerp(Color.red, Color.yellow, healthSlider.value * 2);
        }
        
        //If dead, kill the unit
        if (health <= 0)
        {
            Kill();
        }
    }

    //Destroy the unit
    public void Kill()
    {
        //Spawner.i.SpawnObject(Prefab.Explosion0, gameObject.transform.position);
        //SoundManager.i.PlaySound(Sound.Explosion0, 0.5f);
        Destroy(gameObject);
    }
}
