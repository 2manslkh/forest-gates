using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private LevelGeneration levelGen;
    private FillUpRooms fillUpRooms;
    private UpdateAvailRooms updateAvailRooms;
    public bool RoomsChecked = false;

    Vector2 upPos;
    Vector2 downPos;
    Vector2 rightPos;
    Vector2 leftPos;

    float timeBtwFunctions = 0.5f;
    float startTimeBtwFunctions = 0.5f;

    public int RoomIndex;
    private bool cornerChecked = false;

    // Start is called before the first frame update
    void Start()
    {
        levelGen = GameObject.Find("Level Generation").GetComponent<LevelGeneration>();
        updateAvailRooms = GetComponent<UpdateAvailRooms>();
        // Initialise Room Checker position
        transform.position = new Vector3(levelGen.minX, levelGen.maxY, 0);
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
            if (timeBtwFunctions <= 0)
            {
                //if ((RoomIndex.Equals(0) || RoomIndex.Equals(2) || RoomIndex.Equals(12) || RoomIndex.Equals(15)) && levelGen.RoomBeingChecked == 0 || levelGen.RoomBeingChecked == 3 || levelGen.RoomBeingChecked == 12 || levelGen.RoomBeingChecked == 15)
                //{
                //    Debug.Log("Room index being checked : " + levelGen.RoomBeingChecked);

                //    Debug.Log("Roomindex " + RoomIndex);
                //    //CheckCorners();
                //    StartCoroutine(CheckCorners());
                //    levelGen.RoomBeingChecked += 1;

                //}
                //else if (levelGen.RoomBeingChecked == RoomIndex)
                //{
                Debug.Log("Room index being checked : " + levelGen.RoomBeingChecked);

                //CheckCenterRooms();
                //RoomCheck();
                //Move();
                //if (cornerChecked == false)
                //{
                //    StartCoroutine(CheckCorners());
                //}
                //Debug.Log(68);
                //StartCoroutine(RoomCheck());
                //StartCoroutine(Move());
                Sequence();


                
                //}
                timeBtwFunctions = startTimeBtwFunctions;
            }
            else
            {
                timeBtwFunctions -= Time.deltaTime;
            }
           
        }
    }

    private void Sequence()
    {
        StartCoroutine(Seq());
    }

    private IEnumerator Seq()
    {
        if (cornerChecked == false)
        {
            yield return StartCoroutine(CheckCorners());
        }
        yield return StartCoroutine(CheckCenterRooms());
        yield return StartCoroutine(RoomCheck());
        yield return StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        // Move from top left corner to the right bottom corner
        Vector3 currentPos = transform.position;
        Debug.Log("Room Checker's Current Posistion : " + transform.position);
        if (currentPos.x == levelGen.maxX && currentPos.y == levelGen.minY)
        {
            yield return null;
            //return;
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
        levelGen.RoomBeingChecked += 1;
    }

    IEnumerator CheckCorners()
    {
        Collider2D TopLeftCorner = Physics2D.OverlapCircle(new Vector3(5, 35, 0), 1, levelGen.room);
        Collider2D TopRightCorner = Physics2D.OverlapCircle(new Vector3(35, 35, 0), 1, levelGen.room);
        Collider2D BotLeftCorner = Physics2D.OverlapCircle(new Vector3(5, 5, 0), 1, levelGen.room);
        Collider2D BotRightCorner = Physics2D.OverlapCircle(new Vector3(35, 5, 0), 1, levelGen.room);
        //Debug.Log("checkcorners " + RoomIndex);
        //Debug.Log("where are you" + levelGen.RoomBeingChecked);

        //if ((RoomIndex == 0 || RoomIndex == 3 || RoomIndex == 12 || RoomIndex == 15) && levelGen.RoomBeingChecked == 0)
        //{
        if (TopLeftCorner != null)
        {
            TopLeftCorner.GetComponent<RoomType>().RoomDestruction();
        }
        Instantiate(levelGen.rooms[9], new Vector3(5, 35, 0), Quaternion.identity);
        if (TopRightCorner != null)
        {
            TopRightCorner.GetComponent<RoomType>().RoomDestruction();
        }
        Instantiate(levelGen.rooms[6], new Vector3(35, 35, 0), Quaternion.identity);
        if (BotLeftCorner != null)
        {
            BotLeftCorner.GetComponent<RoomType>().RoomDestruction();
        }
        Instantiate(levelGen.rooms[8], new Vector3(5, 5, 0), Quaternion.identity);
        if (BotRightCorner != null)
        {
            BotRightCorner.GetComponent<RoomType>().RoomDestruction();
        }
        Instantiate(levelGen.rooms[5], new Vector3(35, 5, 0), Quaternion.identity);
        //}
        //else
        //{
        
        //}
        cornerChecked = true;

        yield return null;
        //if (transform.position.x == 5 && transform.position.y == 5)
        //{
        //Instantiate(levelGen.rooms[8], BotLeftCorner.transform.position, Quaternion.identity);
        //}
        //else if (transform.position.y == 5 && transform.position.x == 35)
        //{

        //Instantiate(levelGen.rooms[5], BotRightCorner.transform.position, Quaternion.identity);
        //}
        //else if (transform.position.x == 5 && transform.position.y == 35)
        //{

        //Instantiate(levelGen.rooms[9], TopLeftCorner.transform.position, Quaternion.identity);
        //}
        //else if (transform.position.x == 35 && transform.position.y == 35)
        //{
        //Instantiate(levelGen.rooms[6], TopRightCorner.transform.position, Quaternion.identity);
        //}
        //}
        //else
        //{
        //    yield return null;
        //}

    }

    IEnumerator RoomCheck()
    {
        Collider2D currentSpot = Physics2D.OverlapCircle(transform.position, 1, levelGen.room);
        //if (currentSpot != null)// && (gameObject.name == "Pose" || gameObject.name == "Pose" + " (" + iter + ")"))
        //{
        //updateAvailRooms.availRooms = new List<int> { };

        //if (currentSpot != null && currentSpot.GetComponent<RoomType>().GetInstanceID() == levelGen.generatedRooms[0].GetComponent<RoomType>().GetInstanceID())
        //{
        //    //return;
        //    yield return null;
        //}
        //else if (transform.position.y > 5 && transform.position.y < 35 && transform.position.x > 5 && transform.position.x < 35)
        //{
        //    //updateAvailRooms.updateAvailRooms();
        //    //if (updateAvailRooms.availRooms.Contains(currentSpot.GetComponent<RoomType>().type))
        //    //{
        //    //    return;
        //    //}
        //    //else
        //    //{
        //    //Debug.Log("updateavailrooms" + updateAvailRooms.availRooms.Count);
        //    if (currentSpot != null)
        //    {
        //        currentSpot.GetComponent<RoomType>().RoomDestruction();
        //    }
        //    //while (updateAvailRooms.availRooms.Count == 0)
        //    //{
        //    //    updateAvailRooms.updateAvailRooms();
        //    //}
        //    updateAvailRooms.updateAvailRooms();
        //    int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
        //    Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);
        //    //}
        //}
        //else if (transform.position.x == 5 && transform.position.y == 5)
        //{
        //    yield return null;
        //}
        //else if (transform.position.y == 5 && transform.position.x == 35)
        //{

        //    yield return null;
        //}
        //else if (transform.position.x == 5 && transform.position.y == 35)
        //{

        //    yield return null;
        //}
        //else if (transform.position.x == 35 && transform.position.y == 35)
        //{
        //    yield return null;
        //}

        if (transform.position.x == 5)
        {
            if (transform.position.y == 15)
            {
                if (currentSpot != null)
                {
                    //int roomType = currentSpot.GetComponent<RoomType>().type;
                    //if (levelGen.BotOpeningRoomTypes.Contains(roomType) == false || levelGen.LeftOpeningRoomTypes.Contains(roomType) == true)
                    //{
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    //}
                }
                updateAvailRooms.updateAvailRooms();
                //int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //while (levelGen.LeftOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) || levelGen.BotOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == false)
                //{
                //    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //}
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.LeftOpeningRoomTypes.Contains(r) == true);
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.BotOpeningRoomTypes.Contains(r) == false);
                int rand;
                if (updateAvailRooms.availRooms.Count == 1)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                }
                Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);
            }
            else if (transform.position.y == 25)
            {
                if (currentSpot != null)
                {
                    //int roomType = currentSpot.GetComponent<RoomType>().type;
                    //if (levelGen.TopOpeningRoomTypes.Contains(roomType) == false || levelGen.LeftOpeningRoomTypes.Contains(roomType) == true)
                    //{
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    //}
                }
                updateAvailRooms.updateAvailRooms();
                //int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //while (levelGen.LeftOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) || levelGen.TopOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == false)
                //{
                //    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //}
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.LeftOpeningRoomTypes.Contains(r) == true);
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.TopOpeningRoomTypes.Contains(r) == false);
                int rand;
                if (updateAvailRooms.availRooms.Count == 1)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                }
                Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);

            }
        }

        else if (transform.position.y == 5)
        {
            if (transform.position.x == 15)
            {
                if (currentSpot != null)
                {
                    //int roomType = currentSpot.GetComponent<RoomType>().type;
                    //if (levelGen.LeftOpeningRoomTypes.Contains(roomType) == false || levelGen.BotOpeningRoomTypes.Contains(roomType) == true)
                    //{
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    //}
                }

                updateAvailRooms.updateAvailRooms();
                //for (int i = 0; i < updateAvailRooms.availRooms.Count; i++)
                //{
                //    Debug.Log(updateAvailRooms.availRooms[i]);
                //}
                //int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //while (levelGen.BotOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == true || levelGen.LeftOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == false)
                //{
                //    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //}
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.BotOpeningRoomTypes.Contains(r) == true);
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.LeftOpeningRoomTypes.Contains(r) == false);
                int rand;
                if (updateAvailRooms.availRooms.Count == 1)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                }
                Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 25)
            {
                if (currentSpot != null)
                {
                    //int roomType = currentSpot.GetComponent<RoomType>().type;
                    //if (levelGen.RightOpeningRoomTypes.Contains(roomType) == false || levelGen.BotOpeningRoomTypes.Contains(roomType) == true)
                    //{
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    //}
                }

                updateAvailRooms.updateAvailRooms();
                //int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //while (levelGen.BotOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == true || levelGen.RightOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == false)
                //{
                //    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //}
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.BotOpeningRoomTypes.Contains(r) == true);
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.RightOpeningRoomTypes.Contains(r) == false);
                int rand;
                if (updateAvailRooms.availRooms.Count == 1)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                }
                Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);
            }
        }
        else if (transform.position.x == 35)
        {
            if (transform.position.y == 15)
            {
                if (currentSpot != null)
                {
                    //int roomType = currentSpot.GetComponent<RoomType>().type;
                    //if (levelGen.BotOpeningRoomTypes.Contains(roomType) == false || levelGen.RightOpeningRoomTypes.Contains(roomType) == true)
                    //{
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    //}
                }

                //updateAvailRooms.updateAvailRooms();
                //int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //while (levelGen.RightOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == true || levelGen.BotOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == false)
                //{
                //    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //}
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.RightOpeningRoomTypes.Contains(r) == true);
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.BotOpeningRoomTypes.Contains(r) == false);
                int rand;
                updateAvailRooms.updateAvailRooms();
                if (updateAvailRooms.availRooms.Count == 1)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                }
                Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);
            }
            else if (transform.position.y == 25)
            {
                if (currentSpot != null)
                {
                    //int roomType = currentSpot.GetComponent<RoomType>().type;
                    //if (levelGen.TopOpeningRoomTypes.Contains(roomType) == false || levelGen.RightOpeningRoomTypes.Contains(roomType) == true)
                    //{
                    currentSpot.GetComponent<RoomType>().RoomDestruction();
                    //}
                }

                //updateAvailRooms.updateAvailRooms();
                //int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //while (levelGen.RightOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == true || levelGen.TopOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == false)
                //{
                //    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //}
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.RightOpeningRoomTypes.Contains(r) == true);
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.TopOpeningRoomTypes.Contains(r) == false);
                int rand;
                updateAvailRooms.updateAvailRooms();
                if (updateAvailRooms.availRooms.Count == 1)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                }
                Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);

            }
        }
        else if (transform.position.y == 35)
        {
            if (transform.position.x == 15)
            {
                if (currentSpot != null)
                {
                    int roomType = currentSpot.GetComponent<RoomType>().type;
                    if (levelGen.LeftOpeningRoomTypes.Contains(roomType) == false || levelGen.TopOpeningRoomTypes.Contains(roomType) == true)
                    {
                        currentSpot.GetComponent<RoomType>().RoomDestruction();
                    }
                }

                //updateAvailRooms.updateAvailRooms();
                //int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //while (levelGen.TopOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) || levelGen.LeftOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == false)
                //{
                //    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //}
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.TopOpeningRoomTypes.Contains(r) == true);
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.LeftOpeningRoomTypes.Contains(r) == false);
                int rand;
                updateAvailRooms.updateAvailRooms();
                if (updateAvailRooms.availRooms.Count == 1)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                }
                Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 25)
            {
                if (currentSpot != null)
                {
                    int roomType = currentSpot.GetComponent<RoomType>().type;
                    if (levelGen.RightOpeningRoomTypes.Contains(roomType) == false || levelGen.TopOpeningRoomTypes.Contains(roomType) == true)
                    {
                        currentSpot.GetComponent<RoomType>().RoomDestruction();
                    }
                }

                //updateAvailRooms.updateAvailRooms();
                //int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //while (levelGen.TopOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) || levelGen.RightOpeningRoomTypes.Contains(updateAvailRooms.availRooms[rand]) == false)
                //{
                //    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                //}
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.TopOpeningRoomTypes.Contains(r) == true);
                //updateAvailRooms.availRooms.RemoveAll(r => levelGen.RightOpeningRoomTypes.Contains(r) == false);
                int rand;
                updateAvailRooms.updateAvailRooms();
                if (updateAvailRooms.availRooms.Count == 1)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, updateAvailRooms.availRooms.Count);
                }
                Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);

            }
        }
        yield return null;

        //}
    }

    IEnumerator CheckCenterRooms()
    {
        Collider2D currentSpot = Physics2D.OverlapCircle(transform.position, 1, levelGen.room);
        //if (currentSpot != null)
        //{
        //    //Debug.Log("hello" + currentSpot.GetComponent<RoomType>().GetInstanceID() + transform.position);
        //    //Debug.Log("hi" + levelGen.generatedRooms[0].GetComponent<RoomType>().GetInstanceID() + transform.position);
        //    if (currentSpot.GetComponent<RoomType>().GetInstanceID() == levelGen.generatedRooms[0].GetComponent<RoomType>().GetInstanceID())
        //    {
        //        return;
        //    }
        //    else if (transform.position.y > 5 && transform.position.y < 35 && transform.position.x > 5 && transform.position.x < 35)
        //    {
        //        updateAvailRooms.updateAvailRooms();
        //        if (updateAvailRooms.availRooms.Contains(currentSpot.GetComponent<RoomType>().type))
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            if (currentSpot != null)
        //            {
        //                currentSpot.GetComponent<RoomType>().RoomDestruction();
        //            }
        //            int rand = Random.Range(0, updateAvailRooms.availRooms.Count);
        //            Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);
        //        }
        //    }
        //}
        if (currentSpot != null && currentSpot.GetComponent<RoomType>().GetInstanceID() == levelGen.generatedRooms[0].GetComponent<RoomType>().GetInstanceID())
        {
            //return;
            yield return null;
        }
        else if (transform.position.y > 5 && transform.position.y < 35 && transform.position.x > 5 && transform.position.x < 35)
        {
            //updateAvailRooms.updateAvailRooms();
            //if (updateAvailRooms.availRooms.Contains(currentSpot.GetComponent<RoomType>().type))
            //{
            //    return;
            //}
            //else
            //{
            //Debug.Log("updateavailrooms" + updateAvailRooms.availRooms.Count);
            if (currentSpot != null)
            {
                currentSpot.GetComponent<RoomType>().RoomDestruction();
            }
            //while (updateAvailRooms.availRooms.Count == 0)
            //{
            //    updateAvailRooms.updateAvailRooms();
            //}
            int rand;
            updateAvailRooms.updateAvailRooms();
            if (updateAvailRooms.availRooms.Count == 1)
            {
                rand = 0;
            }
            else
            {
                rand = Random.Range(0, updateAvailRooms.availRooms.Count);
            }
            Instantiate(levelGen.rooms[updateAvailRooms.availRooms[rand]], transform.position, Quaternion.identity);
            //}
        }
        else if (transform.position.x == 5 && transform.position.y == 5)
        {
            yield return null;
        }
        else if (transform.position.y == 5 && transform.position.x == 35)
        {

            yield return null;
        }
        else if (transform.position.x == 5 && transform.position.y == 35)
        {

            yield return null;
        }
        else if (transform.position.x == 35 && transform.position.y == 35)
        {
            yield return null;
        }
    }
}
