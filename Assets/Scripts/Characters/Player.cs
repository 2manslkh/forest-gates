using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	// public CharacterCombat playerCombatManager;
	public PlayerStats playerStats;
	
    public int gold; // Current player gold

	#region Singleton

	public static Player instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	void Start() {
		playerStats.OnHealthReachedZero += Die;
	}

	void Die() {
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	override public void checkIfDead(){
        Debug.Log("CAST CHECK IF DEAD");
        if (characterStats.currentHealth <= 0){

            Debug.Log("DEAD");
            // Transform deathCoin = Instantiate(GameAssets.i.pfDeathCoin,gameObject.transform.position,Quaternion.identity,gameObject.transform);
            // Play death Animation
            // characterAudioSource.PlayOneShot(deathAudio, 1f);
            // StartCoroutine (DeathCoroutine());
            // gameObject.SetActive(false);
        }
    }


}
