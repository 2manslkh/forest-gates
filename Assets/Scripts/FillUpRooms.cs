using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillUpRooms : MonoBehaviour
{
    private LevelGeneration levelGen;
    public static FillUpRooms Instance;

    public bool FilledUp = false;

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
            List<Vector3> extraRoomsPos = levelGen.extraRoomsPos;
            //stopGeneration = false;
            int unspawnedRoomNumber = levelGen.maxRoomNumber - levelGen.currentRoomNumber;
            //for (int i = 0; i < unspawnedRoomNumber; i++)
            while (extraRoomsPos.Count > 0)
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
                    unspawnedRoomNumber -= 1;
                    //else
                    //{
                    //    Instantiate(levelGen.rooms[3], roomPos, Quaternion.identity);

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
        FilledUp = true;
    }
}
