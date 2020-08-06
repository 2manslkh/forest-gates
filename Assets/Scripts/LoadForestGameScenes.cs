using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadForestGameScenes : MonoBehaviour
{
    private bool enterGate;
    // Start is called before the first frame update
    void onAwake()
    {
        enterGate = false;
    }

   void Update(){
        if (enterGate)
        {
             SceneManager.LoadScene("Forest", LoadSceneMode.Single);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ForestGate"))
        {
            enterGate = true;
            Debug.Log(other.tag);
        }

    }
}
