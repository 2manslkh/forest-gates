﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    public Rigidbody2D rb;
    public bool isHit = false;
    public Transform attackPosition;
    public LayerMask playerLayer;

    public float attackRadius;

    public int goldDrop;


    public void dealDamage(){
        // Calculate damage based on character's stats
        int damage = gameObject.GetComponent<CharacterStats>().damage.GetValue();

        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        attackPosition.position = Vector3.Normalize(playerPos - gameObject.transform.position) +  gameObject.transform.position;

        Debug.Log(playerPos);

        // Detect if player is within area of attack
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, playerLayer);


        Debug.Log(enemiesToDamage.Length);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            
            // Check if player's animator isHit
            if (!enemiesToDamage[i].GetComponent<Animator>().GetBool("isHit")){
                // Player to receive damage
                enemiesToDamage[i].GetComponent<CharacterStats>().TakeDamage(damage);
                // Set player's animator flag to isHit
                enemiesToDamage[i].GetComponent<Player>().setHit();

                Debug.Log("Dealing " + damage.ToString() + " damage to player");
                DamagePopup.Create(enemiesToDamage[i].transform.position, damage);
            }
        }
    }   


    override public void checkIfDead(){
        Debug.Log("CAST CHECK IF DEAD");
        if (characterStats.currentHealth <= 0){

            Debug.Log("DEAD");
            Player.instance.gold += goldDrop;
            // Transform deathCoin = Instantiate(GameAssets.i.pfDeathCoin,gameObject.transform.position,Quaternion.identity,gameObject.transform);
            // Play death Animation
            // characterAudioSource.PlayOneShot(deathAudio, 1f);
            StartCoroutine (DeathCoroutine());
            // gameObject.SetActive(false);
            
        }
    }
    


}
