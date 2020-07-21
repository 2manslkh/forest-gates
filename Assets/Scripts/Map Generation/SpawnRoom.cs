﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;
    private bool spawned = false;

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomDetection != null && spawned == false)
        {
            levelGen.currentRoomNumber++;
            spawned = true;
            Debug.Log("Current Room Number : " + levelGen.currentRoomNumber);
        }
        //if (roomDetection == null && levelGen.stopGeneration == true)
        //{
        //    // SPAWN RANDOM ROOM !
        //    int rand = Random.Range(0, levelGen.rooms.Length);
        //    Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
        //    Destroy(gameObject);
        //}
    }
}