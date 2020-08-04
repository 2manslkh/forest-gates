using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBarrier : MonoBehaviour
{
    public GameObject[] boundaries;
    private bool once = false;

    private void Start() {
        once = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(once == false){
            print("triggered");
            once = true;
            foreach (GameObject boundary in boundaries)
            {
                boundary.SetActive(true);
            }
        }

    }
}
