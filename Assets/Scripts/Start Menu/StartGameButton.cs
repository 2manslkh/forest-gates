using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public GameObject StartGameCanvas;
    void OnAwake(){
        StartGameCanvas.SetActive(false);

    }
    public void StartGame(){
        StartGameCanvas.SetActive(true);
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        //SceneManager.LoadScene("1_Bedroom");

    }

}
