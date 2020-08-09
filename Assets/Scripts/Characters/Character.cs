using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats characterStats;
    private int maxHealth;
    public AudioSource characterAudioSource;
    public AudioClip deathAudio;
    private int currentHealth;
    
    void Start(){
        maxHealth = characterStats.maxHealth.GetValue();
        currentHealth = characterStats.currentHealth;
        characterStats.OnHealthReachedZero += checkIfDead;
    }

    
    public Animator characterAnimator;

    public void setHit(){
        characterAnimator.SetBool("isHit",true);
    }

    public void resetHit(){
       characterAnimator.SetBool("isHit",false);
    }

    virtual public void checkIfDead(){
        Debug.Log("CAST CHECK IF DEAD");
        if (characterStats.currentHealth <= 0){

            Debug.Log("DEAD");
            // Transform deathCoin = Instantiate(GameAssets.i.pfDeathCoin,gameObject.transform.position,Quaternion.identity,gameObject.transform);
            // Play death Animation
            // characterAudioSource.PlayOneShot(deathAudio, 1f);
            StartCoroutine (DeathCoroutine());
            // gameObject.SetActive(false);
            
        }
    }
    public IEnumerator DeathCoroutine(){
        Transform deathCoin = Instantiate(GameAssets.i.pfDeathCoin, gameObject.transform.position, Quaternion.identity);
        yield return new WaitWhile (()=> deathCoin.GetComponent<AudioSource>().isPlaying);
        //do something
        gameObject.SetActive(false);
    }
}
