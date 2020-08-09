using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateAvailRooms : MonoBehaviour
{

    private LevelGeneration levelGen;
    private FillUpRooms fillUpRooms;

    Vector2 upPos;
    Vector2 downPos;
    Vector2 rightPos;
    Vector2 leftPos;

    public List<int> availRooms = new List<int> {};

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

    private void updatePosition()
    {
        upPos = new Vector2(transform.position.x, transform.position.y + levelGen.moveAmount);
        downPos = new Vector2(transform.position.x, transform.position.y - levelGen.moveAmount);
        rightPos = new Vector2(transform.position.x + levelGen.moveAmount, transform.position.y);
        leftPos = new Vector2(transform.position.x - levelGen.moveAmount, transform.position.y);

    }

    public void updateAvailRooms()
    {
        updatePosition();
        availRooms = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        Collider2D upDetection = Physics2D.OverlapCircle(upPos, 1, levelGen.room);
        Collider2D downDetection = Physics2D.OverlapCircle(downPos, 1, levelGen.room);
        Collider2D rightDetection = Physics2D.OverlapCircle(rightPos, 1, levelGen.room);
        Collider2D leftDetection = Physics2D.OverlapCircle(leftPos, 1, levelGen.room);

        for (int i = 0; i < availRooms.Count; i++)
        {
            // Debug.Log("availRooms Item " + availRooms[i]);
        }
        if (upDetection != null && levelGen.BotOpeningRoomTypes.Contains(upDetection.GetComponent<RoomType>().type) == true)
        {
            if (availRooms.Count == 0)
            {
                availRooms = levelGen.TopOpeningRoomTypes;
            }
            else
            {
                availRooms = availRooms.Intersect(levelGen.TopOpeningRoomTypes).ToList();
                //for (int i = 0; i < levelGen.TopOpeningRoomTypes.Count; i++)
                //{
                //    if (availRooms.Contains(levelGen.TopOpeningRoomTypes[i]) == false)
                //    {
                //        availRooms.Add(levelGen.TopOpeningRoomTypes[i]);
                //    }
                //}
            }
        }

        Debug.Log("Check Up");
        if (upDetection != null && availRooms.Count > 0 && levelGen.BotOpeningRoomTypes.Contains(upDetection.GetComponent<RoomType>().type) == false)
        {
            //for (int i = 0; i < availRooms.Count; i++)
            //{
            //    if (levelGen.TopOpeningRoomTypes.Contains(availRooms[i]))
            //    {
            //        availRooms.Remove(i);
            //    }
            //}
            availRooms.RemoveAll(r => levelGen.TopOpeningRoomTypes.Contains(r) == true);
        }
        else if (upDetection == null)
        {
            //for (int i = 0; i < availRooms.Count; i++)
            //{
            //    if (levelGen.TopOpeningRoomTypes.Contains(availRooms[i]))
            //    {
            //        availRooms.Remove(i);
            //    }
            //}
            availRooms.RemoveAll(r => levelGen.TopOpeningRoomTypes.Contains(r) == true);

        }

        for (int i = 0; i < availRooms.Count; i++)
        {
            // Debug.Log("availRooms Item " + availRooms[i]);
        }
        if (downDetection != null && levelGen.TopOpeningRoomTypes.Contains(downDetection.GetComponent<RoomType>().type) == true)
        {
            if (availRooms.Count == 0)
            {
                availRooms = levelGen.BotOpeningRoomTypes;
            }
            else
            {
                availRooms = availRooms.Intersect(levelGen.BotOpeningRoomTypes).ToList();
                //for (int i = 0; i < levelGen.BotOpeningRoomTypes.Count; i++)
                //{
                //    if (availRooms.Contains(levelGen.BotOpeningRoomTypes[i]) == false)
                //    {
                //        availRooms.Add(levelGen.BotOpeningRoomTypes[i]);
                //    }
                //}
            }
        }

        for (int i = 0; i < availRooms.Count; i++)
        {
            // Debug.Log("availRooms Item " + availRooms[i]);
        }
        Debug.Log("Check Down");
        if (downDetection != null && availRooms.Count > 0 && levelGen.TopOpeningRoomTypes.Contains(downDetection.GetComponent<RoomType>().type) == false)
        {
            //for (int i = 0; i < availRooms.Count; i++)
            //{
            //    if (levelGen.BotOpeningRoomTypes.Contains(availRooms[i]))
            //    {
            //        availRooms.Remove(i);
            //    }
            //}
            availRooms.RemoveAll(r => levelGen.BotOpeningRoomTypes.Contains(r) == true);

        }
        else if (downDetection == null)
        {
            //for (int i = 0; i < availRooms.Count; i++)
            //{
            //    if (levelGen.BotOpeningRoomTypes.Contains(availRooms[i]))
            //    {
            //        availRooms.Remove(i);
            //    }
            //}
            availRooms.RemoveAll(r => levelGen.BotOpeningRoomTypes.Contains(r) == true);
        }

        for (int i = 0; i < availRooms.Count; i++)
        {
            // Debug.Log("availRooms Item " + availRooms[i]);
        }
        if (rightDetection != null && levelGen.LeftOpeningRoomTypes.Contains(rightDetection.GetComponent<RoomType>().type) == true)
        {
            if (availRooms.Count == 0)
            {
                availRooms = levelGen.RightOpeningRoomTypes;
            }
            else
            {
                availRooms = availRooms.Intersect(levelGen.RightOpeningRoomTypes).ToList();
                //for (int i = 0; i < levelGen.RightOpeningRoomTypes.Count; i++)
                //{
                //    if (availRooms.Contains(levelGen.RightOpeningRoomTypes[i]) == false)
                //    {
                //        availRooms.Add(levelGen.RightOpeningRoomTypes[i]);
                //    }
                //}
            }
        }

        Debug.Log("Check Right");
        if (rightDetection != null && availRooms.Count > 0 && levelGen.LeftOpeningRoomTypes.Contains(rightDetection.GetComponent<RoomType>().type) == false)
        {
            //for (int i = 0; i < availRooms.Count; i++)
            //{
            //    if (levelGen.RightOpeningRoomTypes.Contains(availRooms[i]))
            //    {
            //        availRooms.Remove(i);
            //    }
            //}
            availRooms.RemoveAll(r => levelGen.RightOpeningRoomTypes.Contains(r) == true);
        }
        else if (rightDetection == null)
        {
            //for (int i = 0; i < availRooms.Count; i++)
            //{
            //    if (levelGen.RightOpeningRoomTypes.Contains(availRooms[i]))
            //    {
            //        availRooms.Remove(i);
            //    }
            //}
            availRooms.RemoveAll(r => levelGen.RightOpeningRoomTypes.Contains(r) == true);
        }

        for (int i = 0; i < availRooms.Count; i++)
        {
            // Debug.Log("availRooms Item " + availRooms[i]);
        }
        if (leftDetection != null && levelGen.RightOpeningRoomTypes.Contains(leftDetection.GetComponent<RoomType>().type) == true)
        {
            if (availRooms.Count == 0)
            {
                availRooms = levelGen.LeftOpeningRoomTypes;
            }
            else
            {
                availRooms = availRooms.Intersect(levelGen.LeftOpeningRoomTypes).ToList();
                //for (int i = 0; i < levelGen.LeftOpeningRoomTypes.Count; i++)
                //{
                //    if (availRooms.Contains(levelGen.LeftOpeningRoomTypes[i]) == false)
                //    {
                //        availRooms.Add(levelGen.LeftOpeningRoomTypes[i]);
                //    }
                //}
            }
        }
        
        Debug.Log("Check Left");
        if (leftDetection != null && availRooms.Count > 0 && levelGen.RightOpeningRoomTypes.Contains(leftDetection.GetComponent<RoomType>().type) == false)
        {
            //for (int i = 0; i < availRooms.Count; i++)
            //{
            //    if (levelGen.LeftOpeningRoomTypes.Contains(availRooms[i]))
            //    {
            //        availRooms.Remove(i);
            //    }
            //}
            availRooms.RemoveAll(r => levelGen.LeftOpeningRoomTypes.Contains(r) == true);
        }
        else if (leftDetection == null)
        {
            //for (int i = 0; i < availRooms.Count; i++)
            //{
            //    if (levelGen.LeftOpeningRoomTypes.Contains(availRooms[i]))
            //    {
            //        availRooms.Remove(i);
            //    }
            //}
            availRooms.RemoveAll(r => levelGen.LeftOpeningRoomTypes.Contains(r) == true);
        }
        for (int i = 0; i < availRooms.Count; i++)
        {
            // Debug.Log("availRooms Item " + availRooms[i]);
        }
        //Debug.Log("Available Rooms : " + availRooms.Count);
    }

}
