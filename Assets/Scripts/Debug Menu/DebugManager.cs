using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{

    public Player player;
    public string scene_0;
    public string scene_1;
    public string scene_2;
    public string scene_3;
    public string scene_4;
    public string scene_5;

    void Start(){
        player = Player.instance;
    }

    public void GotoTitle(){
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(scene_0);
    }
    public void GotoBedroom(){
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(scene_1);
    }
    public void GotoTown(){
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(scene_2);
    }
    public void GotoForest(){
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(scene_3);
    }
    public void GotoLake(){
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(scene_4);
    }
    public void GotoCastle(){
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(scene_5);
    }

    public void damagePlayer(){
        player.GetComponent<PlayerStats>().TakeDamage(10);
        Debug.Log(player.GetComponent<PlayerStats>().currentHealth);
    }

    public void healPlayer(){
        player.GetComponent<PlayerStats>().Heal(10);
        Debug.Log(player.GetComponent<PlayerStats>().currentHealth);
    }
}
