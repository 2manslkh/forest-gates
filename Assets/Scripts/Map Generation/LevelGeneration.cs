using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 -->LRTB
    public List<GameObject> generatedRooms;

    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimneBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public int maxRoomNumber;
    public int currentRoomNumber;
    public bool stopGeneration = false;

    public LayerMask room;

    private int downCounter;
    private int upCounter;

    private bool checkSides = true;
    private string prevDir = "";

    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[3], transform.position, Quaternion.identity);
        direction = Random.Range(1, 7);
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimneBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) // Move RIGHT !
        {
            downCounter = 0;
            upCounter = 0;
            if (transform.position.x < maxX)
            {
                //Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                //transform.position = newPos;
                navigate(prevDir);

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                

                //direction = Random.Range(1, 7);
                //if (direction == 3)
                //{
                //    direction = 5;
                //}
                //else if (direction == 4)
                //{
                //    direction = 6;
                //}
            }
            else
            {
                int rand = Random.Range(5, 7);
                direction = rand;
            }
            prevDir = "Right";
        }

        else if (direction == 3 || direction == 4) // Move LEFT !
        {
            downCounter = 0;
            upCounter = 0;
            if (transform.position.x > minX)
            {
                //Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                //transform.position = newPos;
                navigate(prevDir);

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                

                //direction = Random.Range(1, 7);
                //if (direction == 1)
                //{
                //    direction = 5;
                //}
                //else if (direction == 2)
                //{
                //    direction = 6;
                //}
            }
            else
            {
                int rand = Random.Range(5, 7);
                direction = rand;
            }
            prevDir = "Left";
        }

        else if (direction == 5) // Move DOWN !
        {
            downCounter++;
            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                // Check if the room doesn't have bottom opening
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                        
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        while (randBottomRoom == 2)
                        {
                            randBottomRoom = Random.Range(1, 4);
                        }
                        //if (randBottomRoom == 2)
                        //{
                        //    randBottomRoom = 1;
                        //}
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                        
                    }
                }

                //Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                //transform.position = newPos;
                navigate(prevDir);

                // Ensure room always has top opening
                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                // Ensure room does not spawn on top of another room
                //direction = Random.Range(1, 7);
                //while (direction == 6)
                //{
                //    direction = Random.Range(1, 7);
                //}
            }
            prevDir = "Down";
        }

        else if (direction == 6) // Move UP !
        {
            upCounter++;
            if (transform.position.y < maxY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                // Check if the room doesn't have top opening
                if (roomDetection.GetComponent<RoomType>().type != 2 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (upCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                        
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        // int randTopRoom = Random.Range(1, 4);
                        // if (randTopRoom == 2)
                        // {
                        //     randTopRoom = 3;
                        // }
                        int randTopRoom = Random.Range(2, 4);
                        Instantiate(rooms[randTopRoom], transform.position, Quaternion.identity);
                        
                    }
                }

                //Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
                //transform.position = newPos;
                navigate(prevDir);

                // Ensure room always has bottom opening
                int rand = Random.Range(1, 4);
                while (rand == 2)
                {
                    rand = Random.Range(1, 4);
                }
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                // Ensure room does not spawn on top of another room
                //direction = Random.Range(1, 7);
                //while (direction == 5)
                //{
                //    direction = Random.Range(1, 7);
                //}
            }
            prevDir = "Up";
        }
        else if (currentRoomNumber >= maxRoomNumber)
        {
            // STOP LEVEL GENERATION
            stopGeneration = true;
            Debug.Log("Stop Map Generation!");
        }
    }



    private void checkBoundary()
    {
        Vector2 originalPos = transform.position;
        if (originalPos.x < minX || originalPos.x > maxX || originalPos.y < minY || originalPos.y > maxY)
        {
            checkSides = false;
        }
    }

    private void navigate(string prevDir)
    {
        List<Vector2> availDir = new List<Vector2>();
        Vector2 originalPos = transform.position;
        Vector2 upPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
        Vector2 downPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
        Vector2 rightPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
        Vector2 leftPos = new Vector2(transform.position.x - moveAmount, transform.position.y);

        Collider2D upDetection = Physics2D.OverlapCircle(upPos, 1, room);
        Collider2D downDetection = Physics2D.OverlapCircle(downPos, 1, room);
        Collider2D rightDetection = Physics2D.OverlapCircle(rightPos, 1, room);
        Collider2D leftDetection = Physics2D.OverlapCircle(leftPos, 1, room);

        if (prevDir == "")
        {
            int rand = Random.Range(0, 4);
            if (rand == 0)
            {
                navigate("Up");
            }

            else if (rand == 1)
            {
                navigate("Down");
            }

            else if (rand == 2)
            {
                navigate("Right");
            }

            else if (rand == 3)
            {
                navigate("Left");
            }

        }
        else if (prevDir == "Up")
        {
            if (downDetection == null && downPos.y >= minY)
            {
                availDir.Add(downPos);
            }
            if (rightDetection == null && rightPos.x <= maxX)
            {
                availDir.Add(rightPos);
            }
            if (leftDetection == null && leftPos.x >= minX)
            {
                availDir.Add(leftPos);
            }
            if (availDir.Count == 0)
            {
                stopGeneration = true;
            }
            else
            {
                int index = Random.Range(0, availDir.Count);
                transform.position = availDir[index];
                if (availDir[index] == downPos)
                {
                    direction = 5;
                }
                else if (availDir[index] == rightPos)
                {
                    direction = 1;
                }
                else if (availDir[index] == leftPos)
                {
                    direction = 3;
                }
            }
        }

        else if (prevDir == "Down")
        {
            if (upDetection == null && upPos.y <= maxY)
            {
                availDir.Add(upPos);
            }
            if (rightDetection == null && rightPos.x <= maxX)
            {
                availDir.Add(rightPos);
            }
            if (leftDetection == null && leftPos.x >= minX)
            {
                availDir.Add(leftPos);
            }
            if (availDir.Count == 0)
            {
                stopGeneration = true;
            }
            else
            {
                int index = Random.Range(0, availDir.Count);
                transform.position = availDir[index];
                if (availDir[index] == rightPos)
                {
                    direction = 1;
                }
                else if (availDir[index] == leftPos)
                {
                    direction = 3;
                }
                else if (availDir[index] == upPos)
                {
                    direction = 6;
                }
            }
        }

        else if (prevDir == "Left")
        {
            if (upDetection == null && upPos.y < maxY)
            {
                availDir.Add(upPos);
            }
            if (downDetection == null && downPos.y > minY)
            {
                availDir.Add(downPos);
            }
            if (leftDetection == null && leftPos.x > minX)
            {
                availDir.Add(leftPos);
            }
            if (availDir.Count == 0)
            {
                stopGeneration = true;
            } else
            {
                int index = Random.Range(0, availDir.Count);
                transform.position = availDir[index];
                if (availDir[index] == downPos)
                {
                    direction = 5;
                }
                else if (availDir[index] == rightPos)
                {
                    direction = 1;
                }
                else if (availDir[index] == upPos)
                {
                    direction = 6;
                }
            }
        }

        else if (prevDir == "Right")
        {
            if (upDetection == null && upPos.y <= maxY)
            {
                availDir.Add(upPos);
            }
            if (downDetection == null && downPos.y >= minY)
            {
                availDir.Add(downPos);
            }
            if (rightDetection == null && rightPos.x <= maxX)
            {
                availDir.Add(rightPos);
            }
            if (availDir.Count == 0)
            {
                stopGeneration = true;
            }
            else
            {
                int index = Random.Range(0, availDir.Count);
                transform.position = availDir[index];
                if (availDir[index] == downPos)
                {
                    direction = 5;
                }
                else if (availDir[index] == leftPos)
                {
                    direction = 3;
                }
                else if (availDir[index] == upPos)
                {
                    direction = 6;
                }
            }
        }
        else if (upDetection != null && downDetection != null && leftDetection != null && rightDetection != null)
        {
            stopGeneration = true;
        }
        
    }
}
