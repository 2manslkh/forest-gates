﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillUpRooms : MonoBehaviour
{
    private LevelGeneration levelGen;
    public static FillUpRooms Instance;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        levelGen = GameObject.Find("Level Generation").GetComponent<LevelGeneration>();
    }

    public void FillUp()
    {
        List<Vector3> extraRoomsPos = levelGen.extraRoomsPos;
        //stopGeneration = false;
        int unspawnedRoomNumber = levelGen.maxRoomNumber - levelGen.currentRoomNumber;
        for (int i = 0; i < unspawnedRoomNumber; i++)
        {
            int idx = Random.Range(0, extraRoomsPos.Count);
            Vector3 roomPos = extraRoomsPos[idx];
            transform.position = roomPos;

            Vector2 upPos = new Vector2(transform.position.x, transform.position.y + levelGen.moveAmount);
            Vector2 downPos = new Vector2(transform.position.x, transform.position.y - levelGen.moveAmount);
            Vector2 rightPos = new Vector2(transform.position.x + levelGen.moveAmount, transform.position.y);
            Vector2 leftPos = new Vector2(transform.position.x - levelGen.moveAmount, transform.position.y);

            Collider2D currentSpot= Physics2D.OverlapCircle(transform.position, 1, levelGen.room);
            Collider2D upDetection = Physics2D.OverlapCircle(upPos, 1, levelGen.room);
            Collider2D downDetection = Physics2D.OverlapCircle(downPos, 1, levelGen.room);
            Collider2D rightDetection = Physics2D.OverlapCircle(rightPos, 1, levelGen.room);
            Collider2D leftDetection = Physics2D.OverlapCircle(leftPos, 1, levelGen.room);

            List<Collider2D> detectList = new List<Collider2D>();
            detectList.Add(upDetection);
            detectList.Add(downDetection);
            detectList.Add(rightDetection);
            detectList.Add(leftDetection);

            int rand = Random.Range(0, 4);

            //int left_type = leftDetection.GetComponent<RoomType>().type;
            //int right_type = rightDetection.GetComponent<RoomType>().type;
            //int up_type = upDetection.GetComponent<RoomType>().type;
            //int down_type = downDetection.GetComponent<RoomType>().type;


            //Debug.Log("this is" + leftPos + leftDetection + (leftDetection != null));
            if (currentSpot == null)
            {
                if (upDetection != null && upDetection.GetComponent<RoomType>().type != 0)// && (rand == 0 || rand == 1))
                {
                    //int rm_type = upDetection.GetComponent<RoomType>().type;
                    //if (rm_type != 0)
                    //{
                    Instantiate(levelGen.rooms[Random.Range(2, 5)], roomPos, Quaternion.identity);
                    extraRoomsPos.Remove(roomPos);
                    //}

                }

                else if (downDetection != null && downDetection.GetComponent<RoomType>().type != 0 && downDetection.GetComponent<RoomType>().type != 1)// && (rand == 1 || rand == 2))
                {
                    //int rm_type = downDetection.GetComponent<RoomType>().type;
                    //if (rm_type != 0 && rm_type != 1)
                    //{
                    int[] availRoom = { 1, 3, 4 };
                    Instantiate(levelGen.rooms[availRoom[Random.Range(0, availRoom.Length)]], roomPos, Quaternion.identity);
                    extraRoomsPos.Remove(roomPos);

                    //}
                }

                else if (rightDetection != null && rightDetection.GetComponent<RoomType>().type != 4)// && (rand == 2 || rand == 3))
                {
                    //int rm_type = rightDetection.GetComponent<RoomType>().type;
                    //if (rm_type != 4)
                    //{
                        //int[] availRoom = { 1, 3, 4 };
                    Instantiate(levelGen.rooms[Random.Range(0, 4)], roomPos, Quaternion.identity);
                    extraRoomsPos.Remove(roomPos);

                    //}

                }

                else if (leftDetection != null && leftDetection.GetComponent<RoomType>().type != 4)// && (rand == 3 || rand == 0))
                {
                    //int rm_type = leftDetection.GetComponent<RoomType>().type;
                    //if (rm_type != 0 && rm_type != 1)
                    //{
                        //int[] availRoom = { 1, 3, 4 };
                    Instantiate(levelGen.rooms[Random.Range(0, 4)], roomPos, Quaternion.identity);
                    extraRoomsPos.Remove(roomPos);

                    //}
                }
                else
                {
                    Instantiate(levelGen.rooms[3], roomPos, Quaternion.identity);

                }
                // Debug.Log("extraRoomsPos" + extraRoomsPos.Count);


            }


        }
    }
}
