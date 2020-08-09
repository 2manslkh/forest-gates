using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    override public void checkIfDead(){
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
