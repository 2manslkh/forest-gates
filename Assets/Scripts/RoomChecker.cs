using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private LevelGeneration levelGen;
    private FillUpRooms fillUpRooms;
    public bool edgeChecked = false;
    private int iter = 0;

    Vector2 upPos;
    Vector2 downPos;
    Vector2 rightPos;
    Vector2 leftPos;

    List<int> availRooms = new List<int> { };

    float timeBtwFunctions = 0.5f;
    float startTimeBtwFunctions = 0.5f;

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
        if (levelGen.stopGeneration == true && iter < 2 && fillUpRooms.FilledUp == true)
        {
            if (timeBtwFunctions <= 0)
            {
                iter += 1;
                Debug.Log("Iteration no : " + iter);
                CheckCenterRooms();
                
                timeBtwFunctions = startTimeBtwFunctions;
            }
            else
            {
                timeBtwFunctions -= Time.deltaTime;
                RoomCheck();
            }
        }
    }

    public void RoomCheck()
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
                if (transform.position.y == 15 && (levelGen.BotOpeningRoomTypes.Contains(roomType) == false || levelGen.LeftOpeningRoomTypes.Contains(roomType) == true))
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateavailRooms();
                    int rand = Random.Range(0, availRooms.Count);
                    while (levelGen.LeftOpeningRoomTypes.Contains(availRooms[rand]))
                    {
                        rand = Random.Range(0, availRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
                }
                else if (levelGen.TopOpeningRoomTypes.Contains(roomType) == false || levelGen.LeftOpeningRoomTypes.Contains(roomType) == true)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateavailRooms();
                    int rand = Random.Range(0, availRooms.Count);
                    while (levelGen.LeftOpeningRoomTypes.Contains(availRooms[rand]))
                    {
                        rand = Random.Range(0, availRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);

                }
            }
            
            else if (transform.position.y == 5)
            {
                if (transform.position.x == 15 && (levelGen.LeftOpeningRoomTypes.Contains(roomType) == false || levelGen.BotOpeningRoomTypes.Contains(roomType) == true))
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateavailRooms();
                    int rand = Random.Range(0, availRooms.Count);
                    while (levelGen.BotOpeningRoomTypes.Contains(availRooms[rand]))
                    {
                        rand = Random.Range(0, availRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
                }
                else if (levelGen.RightOpeningRoomTypes.Contains(roomType) == false || levelGen.BotOpeningRoomTypes.Contains(roomType) == true)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateavailRooms();
                    int rand = Random.Range(0, availRooms.Count);
                    while (levelGen.BotOpeningRoomTypes.Contains(availRooms[rand]))
                    {
                        rand = Random.Range(0, availRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
                }
            }
            else if (transform.position.x == 35)
            {
                if (transform.position.y == 15 && (levelGen.BotOpeningRoomTypes.Contains(roomType) == false || levelGen.RightOpeningRoomTypes.Contains(roomType) == true))
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateavailRooms();
                    int rand = Random.Range(0, availRooms.Count);
                    while (levelGen.RightOpeningRoomTypes.Contains(availRooms[rand]))
                    {
                        rand = Random.Range(0, availRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
                }
                else if (levelGen.TopOpeningRoomTypes.Contains(roomType) == false || levelGen.RightOpeningRoomTypes.Contains(roomType) == true)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateavailRooms();
                    int rand = Random.Range(0, availRooms.Count);
                    while (levelGen.RightOpeningRoomTypes.Contains(availRooms[rand]))
                    {
                        rand = Random.Range(0, availRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);

                }
            }
            else if (transform.position.y == 35)
            {
                if (transform.position.x == 15 && (levelGen.LeftOpeningRoomTypes.Contains(roomType) == false || levelGen.TopOpeningRoomTypes.Contains(roomType) == true))
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateavailRooms();
                    int rand = Random.Range(0, availRooms.Count);
                    while (levelGen.TopOpeningRoomTypes.Contains(availRooms[rand]))
                    {
                        rand = Random.Range(0, availRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
                }
                if (levelGen.RightOpeningRoomTypes.Contains(roomType) == false || levelGen.TopOpeningRoomTypes.Contains(roomType) == true)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    UpdateavailRooms();
                    int rand = Random.Range(0, availRooms.Count);
                    while (levelGen.TopOpeningRoomTypes.Contains(availRooms[rand]))
                    {
                        rand = Random.Range(0, availRooms.Count);
                    }
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);

                }
            }

            edgeChecked = true;
        }

        
    }

    void CheckCenterRooms()
    {
        Collider2D currentSpot = Physics2D.OverlapCircle(transform.position, 1, levelGen.room);
        if (currentSpot != null)
        {
            //Debug.Log("hello" + currentSpot.GetComponent<RoomType>().GetInstanceID() + transform.position);
            //Debug.Log("hi" + levelGen.generatedRooms[0].GetComponent<RoomType>().GetInstanceID() + transform.position);
            if (currentSpot.GetComponent<RoomType>().GetInstanceID() == levelGen.generatedRooms[0].GetComponent<RoomType>().GetInstanceID())
            {
                return;
            }
            else if (transform.position.y > 5 && transform.position.y < 35 && transform.position.x > 5 && transform.position.x < 35)
            {
                UpdateavailRooms();
                if (availRooms.Contains(currentSpot.GetComponent<RoomType>().type))
                {
                    return;
                }
                else
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    int rand = Random.Range(0, availRooms.Count);
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
                }
            }
        }
    }

    void UpdateavailRooms()
    {
        //availRooms = new List<int>{ };

        Collider2D upDetection = Physics2D.OverlapCircle(upPos, 1, levelGen.room);
        Collider2D downDetection = Physics2D.OverlapCircle(downPos, 1, levelGen.room);
        Collider2D rightDetection = Physics2D.OverlapCircle(rightPos, 1, levelGen.room);
        Collider2D leftDetection = Physics2D.OverlapCircle(leftPos, 1, levelGen.room);


        if (upDetection != null && levelGen.BotOpeningRoomTypes.Contains(upDetection.GetComponent<RoomType>().type))
        {
            if (availRooms.Count == 0)
            {
                availRooms = levelGen.TopOpeningRoomTypes;
            }
            else
            {
                availRooms = availRooms.Intersect(levelGen.TopOpeningRoomTypes).ToList();
            }
        }

        if (downDetection != null && levelGen.TopOpeningRoomTypes.Contains(downDetection.GetComponent<RoomType>().type))
        {
            if (availRooms.Count == 0)
            {
                availRooms = levelGen.BotOpeningRoomTypes;
            }
            else
            {
                availRooms = availRooms.Intersect(levelGen.BotOpeningRoomTypes).ToList();
            }
        }

        if (rightDetection != null && levelGen.LeftOpeningRoomTypes.Contains(rightDetection.GetComponent<RoomType>().type))
        {
            if (availRooms.Count == 0)
            {
                availRooms = levelGen.RightOpeningRoomTypes;
            }
            else
            {
                availRooms = availRooms.Intersect(levelGen.RightOpeningRoomTypes).ToList();
            }
        }

        if (leftDetection != null && levelGen.RightOpeningRoomTypes.Contains(leftDetection.GetComponent<RoomType>().type))
        {
            if (availRooms.Count == 0)
            {
                availRooms = levelGen.LeftOpeningRoomTypes;
            }
            else
            {
                availRooms = availRooms.Intersect(levelGen.LeftOpeningRoomTypes).ToList();
            }
        }
        //Debug.Log("Available Edge Rooms : " + availRooms.Count);
    }
}
