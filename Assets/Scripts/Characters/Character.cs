using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats characterStats;
    private int maxHealth;

    private int currentHealth;
    
    void Start(){
        maxHealth = characterStats.maxHealth.GetValue();
        currentHealth = characterStats.currentHealth;
        characterStats.OnHealthReachedZero += checkIfDead;
    }

    public void checkIfDead(){
        Debug.Log("CAST CHECK IF DEAD");
        if (characterStats.currentHealth <= 0){
            // Play death Animation

            gameObject.SetActive(false);
        }
    }
}
