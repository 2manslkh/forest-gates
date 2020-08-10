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
        StatsHolder.ResetStats();
        StartGameCanvas.SetActive(false);
    }

    public void LoadGame(){
        StartGameCanvas.SetActive(false);
    }

}
