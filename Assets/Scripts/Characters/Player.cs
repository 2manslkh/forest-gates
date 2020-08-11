﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

	public bool debug;
	// public CharacterCombat playerCombatManager;
	public PlayerStats playerStats;
	
    public int gold; // Current player gold

	#region Singleton

	public static Player instance;

	public static bool completedGame = false;

	#endregion
	void Awake(){
		if (!StatsHolder.isNew){
			gold = StatsHolder.gold;
			playerStats.currentHealth = StatsHolder.maxHealth.GetValue();
			playerStats.maxHealth = StatsHolder.maxHealth;
			playerStats.damage = StatsHolder.damage;
		}
		instance = this;
	}
	void Start() {
		if (debug) Instantiate(GameAssets.i.debugUI, Vector3.zero, Quaternion.identity);
		playerStats.OnHealthReachedZero += checkIfDead;
	}

	void Die() {
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	void OnApplicationQuit(){
		Debug.Log("Save");
        StatsHolder.SaveStatsToDisk();
    }

	override public void checkIfDead(){
        Debug.Log("CAST CHECK IF DEAD");
        if (playerStats.currentHealth <= 0){

            Debug.Log("DEAD");

			// gameObject.GetComponent<Animator>().SetBool("isDead", true);
			// Animator[] weapon = gameObject.GetComponentsInChildren<Animator>();
			// Debug.Log(weapon.Length);
			// for (int i = 0; i < weapon.Length; i++){
			// 	weapon[i].GetComponent<Animator>().SetBool("isDead", true);
			// }
			// Instantiate(GameAssets.i.gameOverUI, Vector3.zero, Quaternion.identity);
			// Time.timeScale = 0;
			// weapon.SetBool("isDead", true);

            // Transform deathCoin = Instantiate(GameAssets.i.pfDeathCoin,gameObject.transform.position,Quaternion.identity,gameObject.transform);
            // Play death Animation
            // characterAudioSource.PlayOneShot(deathAudio, 1f);
            StartCoroutine (PlayerDeathCoroutine());
            // gameObject.SetActive(false);
        }
    }

	public IEnumerator PlayerDeathCoroutine(){
        gameObject.GetComponent<Animator>().SetBool("isDead", true);
		Animator[] weapon = gameObject.GetComponentsInChildren<Animator>();
		Debug.Log(weapon.Length);
		for (int i = 0; i < weapon.Length; i++){
			weapon[i].GetComponent<Animator>().SetBool("isDead", true);
		}
        yield return new WaitForSeconds(1.5f);
        //do something
		Time.timeScale = 0;
        Instantiate(GameAssets.i.gameOverUI, Vector3.zero, Quaternion.identity);
    }

}
