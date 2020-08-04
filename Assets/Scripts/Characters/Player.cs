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
}
