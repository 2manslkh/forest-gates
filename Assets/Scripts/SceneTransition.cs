using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "EndScene")
            {
                Destroy(gameObject);
            }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {   
            StatsHolder.SaveStats(Player.instance.playerStats.damage, Player.instance.playerStats.maxHealth, Player.instance.playerStats.maxHealth.GetValue(), Player.instance.gold);
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}
