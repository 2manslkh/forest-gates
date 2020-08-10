using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHolder : MonoBehaviour
{
    public static Stat damage {get;set;}

    public static Stat maxHealth {get;set;}

    public static int currenthealth {get;set;}

    public static int gold {get;set;}

    public static bool isNew = true;

    public GameObject loadGameButton;

    public static StatsHolder i;
    public static void SaveStats(Stat damage, Stat maxHealth, int currenthealth, int gold){
        StatsHolder.damage = damage;
        StatsHolder.maxHealth = maxHealth;
        StatsHolder.currenthealth = currenthealth;
        StatsHolder.gold = gold;
        isNew = false;
    }

    void Awake(){
        Debug.Log("FORRRRRRRRRREST");
        int isNew = PlayerPrefs.GetInt("isNew", 1);
        if (isNew == 1){
            Debug.Log("New Game");
            StatsHolder.isNew = true;
            ResetStats();
            SaveStatsToDisk();
            PlayerPrefs.SetInt("isNew", 1);
        } else {
            Debug.Log("Loading save game...");
            StatsHolder.isNew = false;
            LoadStatsFromDisk();
            loadGameButton.SetActive(true);
        }
        i = this;
    }

    void OnApplicationQuit(){
        StatsHolder.SaveStatsToDisk();
    }

    public static void ResetStats(){
        StatsHolder.damage.baseValue = 10;
        StatsHolder.maxHealth.baseValue = 100;
        StatsHolder.currenthealth = StatsHolder.maxHealth.baseValue;
        StatsHolder.gold = 0;
    }

    public static void SaveStatsToDisk(){
        PlayerPrefs.SetInt("damage", damage.GetValue());
        PlayerPrefs.SetInt("maxHealth", StatsHolder.maxHealth.GetValue());
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("isNew", 0);
    }

    public void LoadStatsFromDisk(){
        Stat damage = new Stat();
        damage.baseValue = PlayerPrefs.GetInt("damage");
        Stat maxHealth = new Stat();
        maxHealth.baseValue = PlayerPrefs.GetInt("maxHealth");

        StatsHolder.damage = damage;
        StatsHolder.maxHealth = maxHealth;
        StatsHolder.gold = PlayerPrefs.GetInt("gold");
    }
}
