using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;
    private bool spawned = false;

    private void Start()
    {
        levelGen = GameObject.Find("Level Generation").GetComponent<LevelGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomDetection != null && spawned == false)
        {
            levelGen.currentRoomNumber++;
            spawned = true;
            // Debug.Log("Current Room Number : " + levelGen.currentRoomNumber);
        }

        // Check if any room is spawned around the current position.
        if (roomDetection == null && levelGen.currentRoomNumber < levelGen.maxRoomNumber && levelGen.stopGeneration == true) //(roomDetection == null && levelGen.stopGeneration == true)
        {
            List<Vector2> posList = new List<Vector2>();
            Vector2 posRight = new Vector2(transform.position.x + levelGen.moveAmount, transform.position.y);
            Vector2 posLeft = new Vector2(transform.position.x - levelGen.moveAmount, transform.position.y);
            Vector2 posUp = new Vector2(transform.position.x, transform.position.y + levelGen.moveAmount);
            Vector2 posDown= new Vector2(transform.position.x, transform.position.y - levelGen.moveAmount);
            posList.Add(posRight);
            posList.Add(posLeft);
            posList.Add(posUp);
            posList.Add(posDown);

            for (int i = 0; i < posList.Count; i++)
            {
                Collider2D nearbyRoom = Physics2D.OverlapCircle(posList[i], 1, whatIsRoom);
                if (nearbyRoom != null)
                {
                    if (posList[i] == posUp && nearbyRoom.GetComponent<RoomType>().type != 0 && nearbyRoom.GetComponent<RoomType>().type != 2)
                    {
                        levelGen.extraRoomsPos.Add(transform.position);
                        break;
                    }
                    else if (posList[i] == posDown && nearbyRoom.GetComponent<RoomType>().type != 0 && nearbyRoom.GetComponent<RoomType>().type != 1)
                    {
                        levelGen.extraRoomsPos.Add(transform.position);
                        break;
                    }
                    else if (posList[i] == posLeft || posList[i] == posRight)
                    {
                        levelGen.extraRoomsPos.Add(transform.position);
                        break;
                    }

                }
            }
            
            //// SPAWN RANDOM ROOM !
            //int rand = Random.Range(0, levelGen.rooms.Length);
            //Instantiate(levelGen.rooms[3], transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
    }
}
