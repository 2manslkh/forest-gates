using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    private LevelGeneration levelGen;

    void Start()
    {
        levelGen = GameObject.Find("Level Generation").GetComponent<LevelGeneration>();
        levelGen.generatedRooms.Add(gameObject);
    }

    public void RoomDestruction() {
        levelGen.generatedRooms.Remove(gameObject);
        Destroy(gameObject);
        Debug.Log("Destroyed : " + gameObject);
    }
}
