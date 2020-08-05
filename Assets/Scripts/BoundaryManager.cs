using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    private Camera cam;
    public Renderer spriteRenderer;
    public GameObject[] boundaries;
    public GameObject[] enemies;

    private void Start() {
        cam = Camera.main;
    }
    void Update()
    {
        // if (cam.tag == "MainCamera")
        // {
        //     if (spriteRenderer.isVisible == false)
        //     {
        //         foreach (GameObject boundary in boundaries)
        //         {
        //             boundary.SetActive(false);
        //         }
        //     }
        //     else
        //     {
        //         foreach (GameObject boundary in boundaries)
        //         {
        //             boundary.SetActive(true);
        //         }
        //     }
        // }
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
