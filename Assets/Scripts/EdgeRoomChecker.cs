using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EdgeRoomChecker : MonoBehaviour
{
    private LevelGeneration levelGen;
    private FillUpRooms fillUpRooms;
    public bool edgeChecked = false;

    Vector2 upPos;
    Vector2 downPos;
    Vector2 rightPos;
    Vector2 leftPos;

    List<int> availEdgeRooms = new List<int> { };

    // Start is called before the first frame update
    void Start()
    {
        levelGen = GameObject.Find("Level Generation").GetComponent<LevelGeneration>();

        upPos = new Vector2(transform.position.x, transform.position.y + levelGen.moveAmount);
        downPos = new Vector2(transform.position.x, transform.position.y - levelGen.moveAmount);
        rightPos = new Vector2(transform.position.x + levelGen.moveAmount, transform.position.y);
        leftPos = new Vector2(transform.position.x - levelGen.moveAmount, transform.position.y);

        fillUpRooms = GameObject.Find("Level Generation").GetComponent<FillUpRooms>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelGen.stopGeneration == true && edgeChecked == false && fillUpRooms.FilledUp == true)
        {
            EdgeRoomCheck();
        }
    }

    public void EdgeRoomCheck()
    {
        Collider2D currentSpot = Physics2D.OverlapCircle(transform.position, 1, levelGen.room);
        if (currentSpot != null)
        {
            int roomType = currentSpot.GetComponent<RoomType>().type;
            if (transform.position.x == 5 && transform.position.y == 5)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
                Instantiate(levelGen.rooms[8], transform.position, Quaternion.identity);
            }
            else if (transform.position.y == 5 && transform.position.x == 35)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
                Instantiate(levelGen.rooms[5], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 5 && transform.position.y == 35)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
                Instantiate(levelGen.rooms[9], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 35 && transform.position.y == 35)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
                Instantiate(levelGen.rooms[6], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 5)
            {
                if (levelGen.LeftOpeningRoomTypes.Contains(roomType))
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateAvailEdgeRooms();
                    int rand = Random.Range(0, availEdgeRooms.Count);
                    while (levelGen.LeftOpeningRoomTypes.Contains(availEdgeRooms[rand]))
                    {
                        rand = Random.Range(0, availEdgeRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availEdgeRooms[rand]], transform.position, Quaternion.identity);

                }
            }
            else if (transform.position.x == 35)
            {
                if (levelGen.RightOpeningRoomTypes.Contains(roomType))
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateAvailEdgeRooms();
                    int rand = Random.Range(0, availEdgeRooms.Count);
                    while (levelGen.RightOpeningRoomTypes.Contains(availEdgeRooms[rand]))
                    {
                        rand = Random.Range(0, availEdgeRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availEdgeRooms[rand]], transform.position, Quaternion.identity);

                }
            }
            else if (transform.position.y == 5)
            {
                if (levelGen.BotOpeningRoomTypes.Contains(roomType))
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateAvailEdgeRooms();
                    int rand = Random.Range(0, availEdgeRooms.Count);
                    while (levelGen.BotOpeningRoomTypes.Contains(availEdgeRooms[rand]))
                    {
                        rand = Random.Range(0, availEdgeRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availEdgeRooms[rand]], transform.position, Quaternion.identity);

                }
            }
            else if (transform.position.y == 35)
            {
                if (levelGen.TopOpeningRoomTypes.Contains(roomType))
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateAvailEdgeRooms();
                    int rand = Random.Range(0, availEdgeRooms.Count);
                    while (levelGen.TopOpeningRoomTypes.Contains(availEdgeRooms[rand]))
                    {
                        rand = Random.Range(0, availEdgeRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availEdgeRooms[rand]], transform.position, Quaternion.identity);

                }
            }
            edgeChecked = true;
        }

        
    }

    void UpdateAvailEdgeRooms()
    {
        //availEdgeRooms = new List<int>{ };

        Collider2D upDetection = Physics2D.OverlapCircle(upPos, 1, levelGen.room);
        Collider2D downDetection = Physics2D.OverlapCircle(downPos, 1, levelGen.room);
        Collider2D rightDetection = Physics2D.OverlapCircle(rightPos, 1, levelGen.room);
        Collider2D leftDetection = Physics2D.OverlapCircle(leftPos, 1, levelGen.room);


        if (upDetection != null && levelGen.BotOpeningRoomTypes.Contains(upDetection.GetComponent<RoomType>().type))
        {
            if (availEdgeRooms.Count == 0)
            {
                availEdgeRooms = levelGen.TopOpeningRoomTypes;
            }
            else
            {
                availEdgeRooms = availEdgeRooms.Intersect(levelGen.TopOpeningRoomTypes).ToList();
            }
        }

        if (downDetection != null && levelGen.TopOpeningRoomTypes.Contains(downDetection.GetComponent<RoomType>().type))
        {
            if (availEdgeRooms.Count == 0)
            {
                availEdgeRooms = levelGen.BotOpeningRoomTypes;
            }
            else
            {
                availEdgeRooms = availEdgeRooms.Intersect(levelGen.BotOpeningRoomTypes).ToList();
            }
        }

        if (rightDetection != null && levelGen.LeftOpeningRoomTypes.Contains(rightDetection.GetComponent<RoomType>().type))
        {
            if (availEdgeRooms.Count == 0)
            {
                availEdgeRooms = levelGen.RightOpeningRoomTypes;
            }
            else
            {
                availEdgeRooms = availEdgeRooms.Intersect(levelGen.RightOpeningRoomTypes).ToList();
            }
        }

        if (leftDetection != null && levelGen.RightOpeningRoomTypes.Contains(leftDetection.GetComponent<RoomType>().type))
        {
            if (availEdgeRooms.Count == 0)
            {
                availEdgeRooms = levelGen.LeftOpeningRoomTypes;
            }
            else
            {
                availEdgeRooms = availEdgeRooms.Intersect(levelGen.LeftOpeningRoomTypes).ToList();
            }
        }
        Debug.Log("Available Edge Rooms : " + availEdgeRooms.Count);
    }
}
