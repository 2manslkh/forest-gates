﻿using UnityEngine;

/* Contains all the stats for a character. */

public class CharacterStats : MonoBehaviour {

	public Stat maxHealth;			// Maximum amount of health

	public int currentHealth;	// Current amount of health

	public Stat damage;
	public Stat armor;

	public Stat currentMana; // used to cast spells
	public Stat maxMana; // Maximum Mana of the player
	public Stat manaRegen; // Mana regeneration per second
	public Stat dodgeChance; // Chance for player to dodge an attack  
	public Stat movementSpeed; // Movement speed of the player
	public Stat attackSpeed; // Attacks per second

	public int agility; // Affects dodge chance, movement speed, attack speed
	public int intelligence; // Affects spell damage, max mana, mana regen
	public int strength; // Affects melee damage
	private AudioSource[] audioClips;

	public event System.Action OnHealthReachedZero;

	public virtual void Awake() {
		currentHealth = maxHealth.GetValue();
		audioClips = GetComponents<AudioSource>();
	}

	// Start with max HP.
	public virtual void Start ()
	{
		
	}

	// Damage the character
	public void TakeDamage (int damage)
	{
		// Subtract the armor value - Make sure damage doesn't go below 0.
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		// Subtract damage from health
		currentHealth -= damage;
		Debug.Log(transform.name + " takes " + damage + " damage.");

		// Set Animation flag isHit to True
		if (damage > 0){
			GetComponent<Animator>().SetBool("isHit", true);
			audioClips[0].Play();
		}

		// If we hit 0. Die.
		if (currentHealth <= 0) 
        {   
            // Cast all subscribed methods when character health = 0
			if (OnHealthReachedZero != null) 
            {
				Debug.Log("Cast OnHealthReachedZero");
				OnHealthReachedZero (); // CAST
			}
		}
	}

	// Heal the character.
	public void Heal (int amount)
	{
		currentHealth += amount;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
	}



}