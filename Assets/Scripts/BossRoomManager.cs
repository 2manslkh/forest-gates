using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoomManager : MonoBehaviour
{
    public GameObject Boss;
    public GameObject levelExit;
    private SceneTransition sceneTransition;
    public GameObject player;

    public bool exitSpawned = false;

    public bool bossKilled = false;

    private void Start()
    {
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);

        int x = Random.Range(-8, 9);
        int y = Random.Range(-8, 9);
        // Instantiate the Boss
        Instantiate(Boss, new Vector3(x, y, 0), Quaternion.identity);

    }

    private void Update()
    {

        if (exitSpawned == false && bossKilled == true)
        {
            SpawnSceneTransition();
        }
    }

    private void SpawnSceneTransition()
    {
        exitSpawned = true;
        // Instantiate level exit
        Instantiate(levelExit, new Vector3(0, 0, 0), Quaternion.identity);

        
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Debug.Log("Current Scene Name : " + currentSceneName);

        sceneTransition = GameObject.FindWithTag("Level Exit").GetComponent<SceneTransition>();

        if (currentSceneName == "Village")
        {
            sceneTransition.sceneToLoad = "Forest";
        }
        else if (currentSceneName == "Forest")
        {
            sceneTransition.sceneToLoad = "Forest Boss";
        }
        else if (currentSceneName == "Forest Boss")
        {
            sceneTransition.sceneToLoad = "Lake";
        }
        else if (currentSceneName == "Lake Boss")
        {
            sceneTransition.sceneToLoad = "Castle";
        }
        else if (currentSceneName == "Castle Boss")
        {
            sceneTransition.sceneToLoad = "EndScene";
        }
    }
}
