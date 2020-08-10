using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public string nextArea;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log(other.tag + " enters portal to " + nextArea);
        if (other.CompareTag("Player")) SceneManager.LoadScene(nextArea);
        
    }
}
