using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public Renderer spriteRenderer;
    public GameObject[] boundaries;
    public GameObject[] enemies;

    private void Awake() {
        foreach (GameObject boundary in boundaries)
        {
            boundary.SetActive(false);
        }
    }
    void Update()
    {
        bool anyEnemyAlive = false;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf == true)
            {
                anyEnemyAlive = true;
                break;
            }
        }
        if (anyEnemyAlive == false)
        {
            foreach (GameObject boundary in boundaries)
            {
                boundary.SetActive(false);
            }
        }
    }
}
