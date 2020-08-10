using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoomManager : MonoBehaviour
{
    public GameObject boss;
    public GameObject levelExit;
    private SceneTransition sceneTransition;
    public GameObject player;

    public bool exitSpawned = false;

    public bool bossKilled = false;

    private void Start()
    {
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);

    }

    private void Update()
    {
        if (exitSpawned == false && !boss.activeSelf)
        {
            SpawnSceneTransition();
        }
    }

    public void SpawnSceneTransition()
    {
        exitSpawned = true;
        // Instantiate level exit
        Instantiate(levelExit, new Vector3(0, 0, 0), Quaternion.identity);

        
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Debug.Log("Current Scene Name : " + currentSceneName);

        sceneTransition = GameObject.FindWithTag("Level Exit").GetComponent<SceneTransition>();

        if (currentSceneName == "2_Town")
        {
            sceneTransition.sceneToLoad = "3_Forest";
        }
        else if (currentSceneName == "3_Forest")
        {
            sceneTransition.sceneToLoad = "4_Forest Boss";
        }
        else if (currentSceneName == "4_Forest Boss")
        {
            sceneTransition.sceneToLoad = "5_Lake";
        }
        else if (currentSceneName == "6_Lake Boss")
        {
            sceneTransition.sceneToLoad = "7_Castle";
        }
        else if (currentSceneName == "8_Castle Boss")
        {
            sceneTransition.sceneToLoad = "9_FinaleEnding";
        }
    }
}
