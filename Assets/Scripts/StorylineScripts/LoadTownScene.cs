using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadTownScene : MonoBehaviour
{
    public void LoadNewScene()
     {
         SceneManager.LoadScene("Town", LoadSceneMode.Single);      
     }
}
