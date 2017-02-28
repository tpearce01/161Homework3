using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    public Slider healthSlider;
    public Image healthSliderFill;
    public float maxHealth;
	public float health;
	public float damageModifier = 1;
    private GameObject bossHealth;
    public float damangeToBoss;


    void Awake()
    {
        bossHealth = GameObject.FindGameObjectWithTag("BossHealth");
    }
    void Start()
    {
        health = maxHealth;
        ModifyHealth(0);
    }

    //Modify unit health and update slider value / color
    public void ModifyHealth(float value)
    {
        health += value * damageModifier;
		if (health >= maxHealth) {
			health = maxHealth;
		}

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
            bossHealth.GetComponent<BossHealth>().modifyBossHealth(damangeToBoss);
            Kill();
        }
    }

    //Destroy the unit
    public void Kill()
    {
        //Spawner.i.SpawnObject(Prefab.Explosion0, gameObject.transform.position);
        //SoundManager.i.PlaySound(Sound.Explosion0, 0.5f);
        print("hurt boss");
        bossHealth.GetComponent<BossHealth>().modifyBossHealth(-damangeToBoss);
        Destroy(gameObject);
    }
}
