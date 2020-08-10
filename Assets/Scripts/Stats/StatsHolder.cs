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

    public static void SaveStats(Stat damage, Stat maxHealth, int currenthealth, int gold){
        StatsHolder.damage = damage;
        StatsHolder.maxHealth = maxHealth;
        StatsHolder.currenthealth = currenthealth;
        StatsHolder.gold = gold;
        isNew = false;
    }

    void Awake(){
        int isNew = PlayerPrefs.GetInt("isNew", 0);
        if (isNew == 1){
            StatsHolder.isNew = true;
        } else {
            StatsHolder.isNew = false;
            StatsHolder.LoadStatsFromDisk();
        }

    }

    public static void SaveStatsToDisk(){
        PlayerPrefs.SetInt("damage", StatsHolder.damage.GetValue());
        PlayerPrefs.SetInt("maxHealth", StatsHolder.maxHealth.GetValue());
        PlayerPrefs.SetInt("gold", StatsHolder.gold);
        PlayerPrefs.SetInt("isNew", 1);
    }

    public static void LoadStatsFromDisk(){
        Stat damage = new Stat();
        damage.baseValue = PlayerPrefs.GetInt("damage");
        Stat maxHealth = new Stat();
        maxHealth.baseValue = PlayerPrefs.GetInt("maxHealth");

        StatsHolder.damage = damage;
        StatsHolder.maxHealth = maxHealth;
        StatsHolder.gold = PlayerPrefs.GetInt("gold");
    }
}
