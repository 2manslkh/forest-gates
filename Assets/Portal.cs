using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public string nextArea;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log(other.tag + " enters portal to " + nextArea);
        if (other.CompareTag("Player")){
            StatsHolder.SaveStats(Player.instance.playerStats.damage, Player.instance.playerStats.maxHealth, Player.instance.playerStats.currentHealth, Player.instance.gold);
            SceneManager.LoadScene(nextArea);
        } 
    }
}
