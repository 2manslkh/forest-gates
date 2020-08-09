using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    private LevelGeneration levelGen;
    public float X;
    public float Y;
    public float Width;
    public float Height;
    public bool firstRoom = false;

    void Start()
    {
        levelGen = GameObject.Find("Level Generation").GetComponent<LevelGeneration>();
        levelGen.generatedRooms.Add(gameObject);
        if (levelGen.generatedRooms.Count == 1)
        {
            GameCamera.instance.CurrentRoom = gameObject;
        }
        X = transform.position.x;
        Y = transform.position.y;
    }

    public void RoomDestruction() {
        levelGen.generatedRooms.Remove(gameObject);
        Destroy(gameObject);
        // Debug.Log("Destroyed : " + gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameCamera.instance.CurrentRoom = gameObject;
        }
    }

    //public Vector3 GetRoomCener()
    //{
    //    return new Vector3(X * Width, Y * Height);
    //}
}
