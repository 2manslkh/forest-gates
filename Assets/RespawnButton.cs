using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnButton : MonoBehaviour
{   
    public string respawnScene;

    public void Respawn(){
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        StatsHolder.SaveStats(Player.instance.playerStats.damage, Player.instance.playerStats.maxHealth, Player.instance.playerStats.currentHealth, Player.instance.gold);
        SceneManager.LoadScene(respawnScene);
        Time.timeScale = 1f;
    }
}
