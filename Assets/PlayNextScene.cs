using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayNextScene : MonoBehaviour
{
    
    public string nextSceneName;
    
    // Start is called before the first frame update
    void onEnable()
    {
        SceneManager.LoadScene(nextSceneName);
    }

}
