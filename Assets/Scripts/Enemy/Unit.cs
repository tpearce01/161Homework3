using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************************************************************
 * class Unit
 * 
 * Function: Parent class to all generic units. A unit must have health and a 
 *      health slider.
 *********************************************************************************/
public class Unit : MonoBehaviour
{
    public Slider healthSlider;         //Slider object to display health value
    public Image healthSliderFill;      //Used to control color of the health bar
    public float maxHealth;             //Maximum health
	public float health;                //Current health
	public float damageModifier = 1;    //Incoming damage modifier
    private bool immortal;              //Determines if the unit can be damaged

    //Initialize health and set health slider
    void Awake()
    {
        health = maxHealth;
        ModifyHealth(0);
    }

    /// <summary>
    /// Modify unit health and visually update health slider. Green is full, red is empty. 
    /// Triggers kill() function
    /// </summary>
    /// <param name="value"></param>
    public void ModifyHealth(float value)
    {
        if (!immortal)
        {
            //Modify health value
            health += value*damageModifier;
            if (health >= maxHealth)
            {
                health = maxHealth;
            }

            healthSlider.value = health/maxHealth;

            //Update slider color depending on health value
            if (healthSlider.value > .50f)
            {
                healthSliderFill.color = Color.Lerp(Color.yellow, Color.green, (healthSlider.value*2) - 1);
            }
            else if (healthSlider.value < 0.5f)
            {
                healthSliderFill.color = Color.Lerp(Color.red, Color.yellow, healthSlider.value*2);
            }

            //If dead, kill the unit
            if (health <= 0)
            {
                Kill();
            }
        }
    }

    /// <summary>
    /// Destroy the unit
    /// </summary>
    public void Kill()
    {
        //SoundManager.i.PlaySound(Sound.Explosion0, 0.5f);
		if (ScreenShake.i != null) {
			ScreenShake.i.StartShake (0.2f, Vector3.one * .5f);
		}
		Spawner.i.SpawnObject (Prefab.Explosion, gameObject.transform.position);
        SoundManager.i.PlaySound(Sound.Explosion, SoundManager.i.volume);
        OnKill();
        Destroy(gameObject);
    }

    /// <summary>
    /// Allows for custom functionality when killed
    /// </summary>
    public virtual void OnKill()
    {
        //Specialized kill function
    }

    /// <summary>
    /// Prevents a unit from taking damage for a duration
    /// </summary>
    /// <param name="duration"></param>
    public void SetImmortal(float duration)
    {
        StartCoroutine(Immortal(duration));
    }

    /// <summary>
    /// Prevents a unit from taking damage for a duration
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator Immortal(float duration)
    {
        immortal = true;
        yield return new WaitForSeconds(duration);
        immortal = false;
    }
}
