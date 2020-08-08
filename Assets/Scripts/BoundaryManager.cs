using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public GameObject[] boundaries;
    public GameObject[] enemies;
    private RoomType roomTypeScript;

    private void Awake() {
        roomTypeScript = GetComponentInParent<RoomType>();
        foreach (GameObject boundary in boundaries)
        {
            boundary.SetActive(false);
        }
    }

    private void Start() {
        if (roomTypeScript.firstRoom)
        {
            print(roomTypeScript.gameObject.name);
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }
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
