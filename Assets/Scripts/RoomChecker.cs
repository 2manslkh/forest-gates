using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private LevelGeneration levelGen;
    private FillUpRooms fillUpRooms;
    public bool RoomsChecked = false;
    private int iter = 0;

    Vector2 upPos;
    Vector2 downPos;
    Vector2 rightPos;
    Vector2 leftPos;

    List<int> availRooms = new List<int> { };

    float timeBtwFunctions = 0.5f;
    float startTimeBtwFunctions = 0.5f;

    public int RoomIndex;

    // Start is called before the first frame update
    void Start()
    {
        levelGen = GameObject.Find("Level Generation").GetComponent<LevelGeneration>();
        //transform.position = new Vector3(levelGen.minX, levelGen.maxY, 0);
        upPos = new Vector2(transform.position.x, transform.position.y + levelGen.moveAmount);
        downPos = new Vector2(transform.position.x, transform.position.y - levelGen.moveAmount);
        rightPos = new Vector2(transform.position.x + levelGen.moveAmount, transform.position.y);
        leftPos = new Vector2(transform.position.x - levelGen.moveAmount, transform.position.y);

        fillUpRooms = GameObject.Find("Level Generation").GetComponent<FillUpRooms>();

    }

    // Update is called once per frame
    void Update()
    {
        if (levelGen.stopGeneration == true && levelGen.RoomBeingChecked < levelGen.maxRoomNumber && fillUpRooms.FilledUp == true)
        {
            if (levelGen.RoomBeingChecked == RoomIndex)
            {
                if (timeBtwFunctions <= 0)
                {
                    Debug.Log("Room index being checked : " + levelGen.RoomBeingChecked);
                    //if (iter % 2 == 0)
                    //{

                    //CheckCenterRooms();
                    //coroutine = RoomCheck();
                    //StartCoroutine(coroutine);
                    RoomCheck();

                    levelGen.RoomBeingChecked += 1;

                    //Move();

                    //Debug.Log("Checking non center rooms");
                    //}
                    //else
                    //{


                    //Debug.Log("Checking Center Rooms");
                    //}
                    timeBtwFunctions = startTimeBtwFunctions;
                }
                else
                {
                    timeBtwFunctions -= Time.deltaTime;
                }
            }
        }
        
    }
    void Move()
    {
        // Move from top left corner to the right bottom corner
        Vector3 currentPos = transform.position;
        Debug.Log("Room Checker's Current Posistion : " + transform.position);
        if (currentPos.x == levelGen.maxX && currentPos.y == levelGen.minY)
        {
            //yield return null;
            return;
        }
        else if (currentPos.x < levelGen.maxX)
        {
            transform.position = new Vector3(transform.position.x + levelGen.moveAmount, transform.position.y, 0);
        }
        else if (currentPos.y > levelGen.minY)
        {
            transform.position = new Vector3(levelGen.minX, transform.position.y - levelGen.moveAmount, 0);
        }
        Debug.Log("Room Checker's Moved Posistion : " + transform.position);
    }

    void RoomCheck()
    {
        Collider2D currentSpot = Physics2D.OverlapCircle(transform.position, 1, levelGen.room);
        //if (currentSpot != null)// && (gameObject.name == "Pose" || gameObject.name == "Pose" + " (" + iter + ")"))
        //{
        //availRooms = new List<int> { };
        int roomType = currentSpot.GetComponent<RoomType>().type;

        if (currentSpot != null && currentSpot.GetComponent<RoomType>().GetInstanceID() == levelGen.generatedRooms[0].GetComponent<RoomType>().GetInstanceID())
        {
            return;
        }
        else if (transform.position.y > 5 && transform.position.y < 35 && transform.position.x > 5 && transform.position.x < 35)
        {
            UpdateAvailRooms();
            if (availRooms.Contains(currentSpot.GetComponent<RoomType>().type))
            {
                return;
            }
            else
            {
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                int rand = Random.Range(0, availRooms.Count);
                Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
            }
        }
        else if (transform.position.x == 5 && transform.position.y == 5)
        {
            if (currentSpot != null)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
            }
            Instantiate(levelGen.rooms[8], transform.position, Quaternion.identity);
        }
        else if (transform.position.y == 5 && transform.position.x == 35)
        {
            if (currentSpot != null)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
            }
            Instantiate(levelGen.rooms[5], transform.position, Quaternion.identity);
        }
        else if (transform.position.x == 5 && transform.position.y == 35)
        {
            if (currentSpot != null)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
            }
            Instantiate(levelGen.rooms[9], transform.position, Quaternion.identity);
        }
        else if (transform.position.x == 35 && transform.position.y == 35)
        {
            if (currentSpot != null)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
            }
            Instantiate(levelGen.rooms[6], transform.position, Quaternion.identity);
        }
        else if (transform.position.x == 5)
        {
            if (transform.position.y == 15 && (levelGen.BotOpeningRoomTypes.Contains(roomType) == false || levelGen.LeftOpeningRoomTypes.Contains(roomType) == true))
            {
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                UpdateAvailRooms();
                int rand = Random.Range(0, availRooms.Count);
                while (levelGen.LeftOpeningRoomTypes.Contains(availRooms[rand]) || levelGen.BotOpeningRoomTypes.Contains(availRooms[rand]) == false)
                {
                    rand = Random.Range(0, availRooms.Count);
                }
                Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
            }
            else if (transform.position.y == 25 && (levelGen.TopOpeningRoomTypes.Contains(roomType) == false || levelGen.LeftOpeningRoomTypes.Contains(roomType) == true))
            {
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                UpdateAvailRooms();
                int rand = Random.Range(0, availRooms.Count);
                while (levelGen.LeftOpeningRoomTypes.Contains(availRooms[rand]) || levelGen.TopOpeningRoomTypes.Contains(availRooms[rand]) == false)
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
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                UpdateAvailRooms();
                int rand = Random.Range(0, availRooms.Count);
                while (levelGen.BotOpeningRoomTypes.Contains(availRooms[rand]) == true || levelGen.LeftOpeningRoomTypes.Contains(availRooms[rand]) == false)
                {
                    rand = Random.Range(0, availRooms.Count);
                }
                Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 25 && (levelGen.RightOpeningRoomTypes.Contains(roomType) == false || levelGen.BotOpeningRoomTypes.Contains(roomType) == true))
            {
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                UpdateAvailRooms();
                int rand = Random.Range(0, availRooms.Count);
                while (levelGen.BotOpeningRoomTypes.Contains(availRooms[rand]) == true || levelGen.RightOpeningRoomTypes.Contains(availRooms[rand]) == false)
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
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                UpdateAvailRooms();
                int rand = Random.Range(0, availRooms.Count);
                while (levelGen.RightOpeningRoomTypes.Contains(availRooms[rand]) == true || levelGen.BotOpeningRoomTypes.Contains(availRooms[rand]) == false)
                {
                    rand = Random.Range(0, availRooms.Count);
                }
                Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
            }
            else if (transform.position.y == 25 && (levelGen.TopOpeningRoomTypes.Contains(roomType) == false || levelGen.RightOpeningRoomTypes.Contains(roomType) == true))
            {
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                UpdateAvailRooms();
                int rand = Random.Range(0, availRooms.Count);
                while (levelGen.RightOpeningRoomTypes.Contains(availRooms[rand]) == true || levelGen.TopOpeningRoomTypes.Contains(availRooms[rand]) == false)
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
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                UpdateAvailRooms();
                int rand = Random.Range(0, availRooms.Count);
                while (levelGen.TopOpeningRoomTypes.Contains(availRooms[rand]) || levelGen.LeftOpeningRoomTypes.Contains(availRooms[rand]) == false)
                {
                    rand = Random.Range(0, availRooms.Count);
                }
                Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 25 && (levelGen.RightOpeningRoomTypes.Contains(roomType) == false || levelGen.TopOpeningRoomTypes.Contains(roomType) == true))
            {
                if (currentSpot != null)
                {
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                }
                UpdateAvailRooms();
                int rand = Random.Range(0, availRooms.Count);
                while (levelGen.TopOpeningRoomTypes.Contains(availRooms[rand]) || levelGen.RightOpeningRoomTypes.Contains(availRooms[rand]) == false)
                {
                    rand = Random.Range(0, availRooms.Count);
                }
                Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);

            }
        }


        //}
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
                UpdateAvailRooms();
                if (availRooms.Contains(currentSpot.GetComponent<RoomType>().type))
                {
                    return;
                }
                else
                {
                    if (currentSpot != null)
                    {
                        currentSpot.GetComponent<RoomType>().RoomDestruction();
                    }
                    int rand = Random.Range(0, availRooms.Count);
                    Instantiate(levelGen.rooms[availRooms[rand]], transform.position, Quaternion.identity);
                }
            }
        }
    }

    void UpdateAvailRooms()
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
        else if (upDetection != null && availRooms.Count > 0 && levelGen.BotOpeningRoomTypes.Contains(upDetection.GetComponent<RoomType>().type) == false)
        {
            for (int i = 0; i < availRooms.Count; i++)
            {
                if (levelGen.TopOpeningRoomTypes.Contains(availRooms[i]))
                {
                    availRooms.Remove(i);
                }
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
        else if (downDetection != null && availRooms.Count > 0 && levelGen.TopOpeningRoomTypes.Contains(downDetection.GetComponent<RoomType>().type) == false)
        {
            for (int i = 0; i < availRooms.Count; i++)
            {
                if (levelGen.BotOpeningRoomTypes.Contains(availRooms[i]))
                {
                    availRooms.Remove(i);
                }
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
        else if (rightDetection != null && availRooms.Count > 0 && levelGen.LeftOpeningRoomTypes.Contains(rightDetection.GetComponent<RoomType>().type) == false)
        {
            for (int i = 0; i < availRooms.Count; i++)
            {
                if (levelGen.RightOpeningRoomTypes.Contains(availRooms[i]))
                {
                    availRooms.Remove(i);
                }
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
        else if (leftDetection != null && availRooms.Count > 0 && levelGen.RightOpeningRoomTypes.Contains(leftDetection.GetComponent<RoomType>().type) == false)
        {
            for (int i = 0; i < availRooms.Count; i++)
            {
                if (levelGen.LeftOpeningRoomTypes.Contains(availRooms[i]))
                {
                    availRooms.Remove(i);
                }
            }
        }

        Debug.Log("Available Rooms : " + availRooms.Count);
    }
}
