using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    // index 0 --> LR, index 1 --> LRD, index 2 --> LRU, index 3 -->LRUD, index 4 -->UD
    // index 5 --> LU, index 6 --> LD, index 7 -->LUD, index 8 --> RU, index 9 -->RD, index 10 -->RUD
    public List<int> LeftOpeningRoomTypes = new List<int> {0, 1, 2, 3, 5, 6, 7};
    public List<int> RightOpeningRoomTypes = new List<int> { 0, 1, 2, 3, 8, 9, 10 };
    public List<int> TopOpeningRoomTypes = new List<int> { 2, 3, 4, 5, 7, 8, 10 };
    public List<int> BotOpeningRoomTypes = new List<int> { 1, 3, 4, 6, 7, 9, 10 };

    public GameObject Boss;
    public GameObject[] rooms; 
    public List<GameObject> generatedRooms;
    public List<Vector3> extraRoomsPos;
    public GameObject levelExit;
    private SceneTransition sceneTransition;
    public GameObject player;
    private FillUpRooms fillUpRooms;

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
    public bool exitSpawned = false;

    public LayerMask room;

    private string prevDir = "";


    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[Random.Range(0, 11)], transform.position, Quaternion.identity);
        Instantiate(player, transform.position, Quaternion.identity);
        direction = Random.Range(1, 7);
        fillUpRooms = GetComponent<FillUpRooms>();
    }

    private void Update()
    {
        if (maxRoomNumber == currentRoomNumber)
        {
            stopGeneration = true;
            if (exitSpawned == false)
            {
                SpawnSceneTransition();
            }
        }
        else if (maxRoomNumber > currentRoomNumber && stopGeneration == true)
        {
            fillUpRooms.FillUp();
            //FillUpRooms.Instance.FillUp();
        }
        else if (timeBtwRoom <= 0 && stopGeneration == false)
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
            if (transform.position.x <= maxX)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                // Check if the room doesn't have bottom opening
                int detectedRoomType = roomDetection.GetComponent<RoomType>().type;
                if (RightOpeningRoomTypes.Contains(detectedRoomType) == false)
                {
                    if (prevDir == "Right")//(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randLeftRoom = Random.Range(0, LeftOpeningRoomTypes.Count);
                        Instantiate(rooms[LeftOpeningRoomTypes[randLeftRoom]], transform.position, Quaternion.identity);

                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randRightRoom = Random.Range(0, RightOpeningRoomTypes.Count);
                        Instantiate(rooms[RightOpeningRoomTypes[randRightRoom]], transform.position, Quaternion.identity);

                    }
                }
                transform.position = new Vector3(transform.position.x + moveAmount, transform.position.y, 0);
                Navigate(prevDir, direction);

                int rand = Random.Range(0, LeftOpeningRoomTypes.Count);
                {
                    Instantiate(rooms[LeftOpeningRoomTypes[rand]], transform.position, Quaternion.identity);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x + moveAmount, transform.position.y, 0);
                Navigate(prevDir, direction);
            }
            prevDir = "Right";
        }

        else if (direction == 3 || direction == 4) // Move LEFT !
        {
            if (transform.position.x >= minX)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                // Check if the room doesn't have bottom opening
                int detectedRoomType = roomDetection.GetComponent<RoomType>().type;
                if (LeftOpeningRoomTypes.Contains(detectedRoomType) == false)
                {
                    if (prevDir == "Right")//(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randRightRoom = Random.Range(0, RightOpeningRoomTypes.Count);
                        Instantiate(rooms[RightOpeningRoomTypes[randRightRoom]], transform.position, Quaternion.identity);

                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randLeftRoom = Random.Range(0, LeftOpeningRoomTypes.Count);
                        Instantiate(rooms[LeftOpeningRoomTypes[randLeftRoom]], transform.position, Quaternion.identity);

                    }
                }
                transform.position = new Vector3(transform.position.x - moveAmount, transform.position.y, 0);
                Navigate(prevDir, direction);

                int rand = Random.Range(0, RightOpeningRoomTypes.Count);
                if (stopGeneration == false)
                {
                    Instantiate(rooms[RightOpeningRoomTypes[rand]], transform.position, Quaternion.identity);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x - moveAmount, transform.position.y, 0);
                Navigate(prevDir, direction);
            }
            prevDir = "Left";
        }

        else if (direction == 5) // Move DOWN !
        {
            if (transform.position.y >= minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                // Check if the room doesn't have bottom opening
                int detectedRoomType = roomDetection.GetComponent<RoomType>().type;
                if (BotOpeningRoomTypes.Contains(detectedRoomType) == false)
                {
                    if (prevDir == "Down")//(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randTopRoom = Random.Range(0, TopOpeningRoomTypes.Count);                            
                        Instantiate(rooms[TopOpeningRoomTypes[randTopRoom]], transform.position, Quaternion.identity);
                        
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randBottomRoom = Random.Range(0, BotOpeningRoomTypes.Count);
                        Instantiate(rooms[BotOpeningRoomTypes[randBottomRoom]], transform.position, Quaternion.identity);
                        
                    }
                }
                transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount, 0);
                Navigate(prevDir, direction);

                // Ensure room always has top opening
                int rand = Random.Range(0, TopOpeningRoomTypes.Count);
                if (stopGeneration == false)
                {
                    Instantiate(rooms[TopOpeningRoomTypes[rand]], transform.position, Quaternion.identity);
                }
            }
            prevDir = "Down";
        }

        else if (direction == 6) // Move UP !
        {
            if (transform.position.y <= maxY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                // Check if the room doesn't have top opening
                int detectedRoomType = roomDetection.GetComponent<RoomType>().type;
                if (TopOpeningRoomTypes.Contains(detectedRoomType) == false)
                {
                    if (prevDir == "UP")//upCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randBottomRoom = Random.Range(0, BotOpeningRoomTypes.Count);
                        Instantiate(rooms[BotOpeningRoomTypes[randBottomRoom]], transform.position, Quaternion.identity);
                        
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randTopRoom = Random.Range(0, TopOpeningRoomTypes.Count);
                        Instantiate(rooms[TopOpeningRoomTypes[randTopRoom]], transform.position, Quaternion.identity);
                        
                    }
                }
                transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount, 0);
                Navigate(prevDir, direction);

                // Ensure room always has bottom opening
                int rand = Random.Range(0, BotOpeningRoomTypes.Count);
                if (stopGeneration == false)
                {
                    Instantiate(rooms[BotOpeningRoomTypes[rand]], transform.position, Quaternion.identity);
                }

            }
            prevDir = "Up";
        }
        else if (currentRoomNumber >= maxRoomNumber)
        {
            // STOP LEVEL GENERATION
            stopGeneration = true;
            // Debug.Log("Stop Map Generation!");
        }
    }

    private void Navigate(string prevDir, int curDir)
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
                Navigate("Up", curDir);
            }

            else if (rand == 1)
            {
                Navigate("Down", curDir);
            }

            else if (rand == 2)
            {
                Navigate("Right", curDir);
            }

            else if (rand == 3)
            {
                Navigate("Left", curDir);
            }
        }

        else if (direction == 6)//(prevDir == "Up")
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
                // Debug.Log("Previous Direction :" + prevDir);
                // Debug.Log("Stop position :" + transform.position);
                stopGeneration = true;
            }
            else
            {
                int index = Random.Range(0, availDir.Count);
                //transform.position = availDir[index];
                if (availDir[index] == upPos)
                {
                    direction = 6;
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

        else if (direction == 5) //(prevDir == "Down")
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
            // Debug.Log("AvailDir Count : " + availDir.Count);
            if (availDir.Count == 0)
            {
                // Debug.Log("Previous Direction :" + prevDir);
                // Debug.Log("Stop position :" + transform.position);
                stopGeneration = true;
            }
            else
            {
                int index = Random.Range(0, availDir.Count);
                //transform.position = availDir[index];
                if (availDir[index] == rightPos)
                {
                    direction = 1;
                }
                else if (availDir[index] == leftPos)
                {
                    direction = 3;
                }
                else if (availDir[index] == downPos)
                {
                    direction = 5;
                }
            }
        }

        else if (direction == 3 || direction == 4) //(prevDir == "Left")
        {
            if (upDetection == null && upPos.y <= maxY)
            {
                availDir.Add(upPos);
            }
            if (downDetection == null && downPos.y >= minY)
            {
                availDir.Add(downPos);
            }
            if (leftDetection == null && leftPos.x >= minX)
            {
                availDir.Add(leftPos);
            }
            // Debug.Log("AvailDir Count : " + availDir.Count);
            if (availDir.Count == 0)
            {
                // Debug.Log("Previous Direction :" + prevDir);
                // Debug.Log("Stop position :" + transform.position);
                stopGeneration = true;
            }
            else
            {
                int index = Random.Range(0, availDir.Count);
                //transform.position = availDir[index];
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

        else if (direction == 1 || direction == 2) //(prevDir == "Right")
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
            // Debug.Log("AvailDir Count : " + availDir.Count);
            if (availDir.Count == 0)
            {
                // Debug.Log("Previous Direction :" + prevDir);
                // Debug.Log("Stop position :" + transform.position);
                stopGeneration = true;
            }
            else
            {
                int index = Random.Range(0, availDir.Count);
                //transform.position = availDir[index];
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
        // Debug.Log(" Current Direction : " + direction);
    }

    private void SpawnSceneTransition()
    {
        Vector2 lastRoomPos = generatedRooms[generatedRooms.Count-1].transform.position;
        // Instantiate the Boss
        Instantiate(Boss, lastRoomPos, Quaternion.identity);
        // Instantiate level exit
        Instantiate(levelExit, lastRoomPos, Quaternion.identity);
        exitSpawned = true;

        string currentSceneName = SceneManager.GetActiveScene().name;
        // Debug.Log("Current Scene Name : " + currentSceneName);

        sceneTransition = GameObject.FindWithTag("Level Exit").GetComponent<SceneTransition>();

        if (currentSceneName == "Village")
        {
            sceneTransition.sceneToLoad = "Forest";
        }
        else if (currentSceneName == "Forest")
        {
            sceneTransition.sceneToLoad = "Lake";
        }
        else if (currentSceneName == "Lake")
        {
            sceneTransition.sceneToLoad = "Castle";
        }
        else if (currentSceneName == "Castle")
        {
            sceneTransition.sceneToLoad = "EndScene";
        }

    }

}
