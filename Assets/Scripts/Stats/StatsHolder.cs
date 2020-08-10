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

}
