using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour {

    public string username;

    public int maxHealth = 1000;
    public int currentHealth;

    public int maxMana = 1000;
    public int currentMana;

    public Slider healthBar;
    public List<Spell> spells;
    //public List<Effects> effects;


	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        currentMana = maxMana;
        UpdateBars();
        
	}

    public void TakeDamage(Character shooter, int amt)
    {

        currentHealth -= amt;
        UpdateBars();
        if (currentHealth <= 0)
        {
            //Die
            Debug.Log(shooter.username + " killed " + this.username);
        }
    }

    public void UpdateBars()
    {
        
        healthBar.value = ((float)currentHealth / maxHealth);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
