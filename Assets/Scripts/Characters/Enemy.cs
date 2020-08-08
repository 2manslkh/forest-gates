using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    public Rigidbody2D rb;
    public Animator enemyAnimator;
    public bool isHit = false;
    public Transform attackPosition;
    public LayerMask playerLayer;
    public void setHit(){
        enemyAnimator.SetBool("isHit",true);
    }

    public void resetHit(){
       enemyAnimator.SetBool("isHit",false);
    }

    public void dealDamage(){
        // Calculate damage based on character's stats
        int damage = gameObject.GetComponent<CharacterStats>().damage.GetValue();

        // Detect if player is within area of attack
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, 1, playerLayer);

        Debug.Log(enemiesToDamage.Length);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            Debug.Log("Dealing " + damage.ToString() + " damage to player");
            DamagePopup.Create(enemiesToDamage[i].transform.position, damage, "miss");

            
            
            // Set player's animator flag to isHit
            if (!enemiesToDamage[i].GetComponent<Animator>().GetBool("isHit")){
                // Player to receive damage
                enemiesToDamage[i].GetComponent<CharacterStats>().TakeDamage(damage);
                enemiesToDamage[i].GetComponent<Enemy>().setHit();
            }
        }


    }
}
