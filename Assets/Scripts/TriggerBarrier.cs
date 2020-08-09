using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBarrier : MonoBehaviour
{
    public GameObject[] boundaries;
    private bool once;
    public bool enemyMove;

    private void Start() {
        once = false;
        enemyMove = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if((other.tag == "Player") && (once == false)){
            enemyMove = true;
            once = true;
            foreach (GameObject boundary in boundaries)
            {
                boundary.SetActive(true);
            }
        }

    }
}
