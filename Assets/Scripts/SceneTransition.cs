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
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}
