using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public GameObject[] boundaries;
    public GameObject[] enemies;
    private RoomType roomTypeScript;
    public TriggerBarrier triggerBarrierScript;
    public bool moveOnce;

    private void Awake() {
        roomTypeScript = GetComponentInParent<RoomType>();
        foreach (GameObject boundary in boundaries)
        {
            boundary.SetActive(false);
        }
        foreach (GameObject enemy in enemies)
        {
            // enemy.GetComponent<Animator>().enabled = false;
            enemy.SetActive(false);
        }
    }

    private void Start() {
        moveOnce = false;
        if (roomTypeScript.firstRoom)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }
        }
    }
    void Update()
    {
        if(moveOnce == false)
        {
            if(triggerBarrierScript.enemyMove)
            {
                foreach (GameObject enemy in enemies)
                {
                    // enemy.GetComponent<Animator>().enabled = true;
                    enemy.SetActive(true);
                }
                print("enable room " + triggerBarrierScript.gameObject.name);
                moveOnce = true;
            }
        }
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
