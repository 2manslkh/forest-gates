using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinePlayer : MonoBehaviour
{
    private LevelGeneration levelGen;

    BoxCollider2D boxCollider;
    EdgeCollider2D edgeCollider;
    private bool enemiesCleared = false;
    private PlayerController playerController;
    private GameObject player;

    //public List<Collider2D> roomDetection;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
    }
    void Start()
    {
        levelGen = GameObject.Find("Level Generation").GetComponent<LevelGeneration>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player = playerController.gameObject;
    }

    private void Update()
    {
        //if (Mathf.Abs(player.transform.position.x - transform.position.x) < 5.0 || Mathf.Abs(player.transform.position.y - transform.position.y) < 5.0)
        if (gameObject == GameCamera.Instance.CurrentRoom)
        {
            if (enemiesCleared == false && Input.GetKey(KeyCode.Space))
            {
                OnRoomClear();
                Debug.Log("Confinement broken!");
            }
        }
    }


    private void OnRoomClear()
    {
        //float check = player.transform.position.x - transform.position.x;
        //Debug.Log("chekcing" + check);
        //Destroy(edgeColllider);
        edgeCollider.isTrigger = true;
        enemiesCleared = true;

        Vector2 originalPos = transform.position;
        Vector2 upPos = new Vector2(transform.position.x, transform.position.y + levelGen.moveAmount);
        Vector2 downPos = new Vector2(transform.position.x, transform.position.y - levelGen.moveAmount);
        Vector2 rightPos = new Vector2(transform.position.x + levelGen.moveAmount, transform.position.y);
        Vector2 leftPos = new Vector2(transform.position.x - levelGen.moveAmount, transform.position.y);

        Collider2D upDetection = Physics2D.OverlapCircle(upPos, 1, levelGen.room);
        Collider2D downDetection = Physics2D.OverlapCircle(downPos, 1, levelGen.room);
        Collider2D rightDetection = Physics2D.OverlapCircle(rightPos, 1, levelGen.room);
        Collider2D leftDetection = Physics2D.OverlapCircle(leftPos, 1, levelGen.room);

        if (upDetection != null && upDetection.GetComponent<RoomType>().type != 0 && upDetection.GetComponent<RoomType>().type != 2)
        {
            upDetection.GetComponent<EdgeCollider2D>().isTrigger = true;
        }

        if (downDetection != null && downDetection.GetComponent<RoomType>().type != 0 && downDetection.GetComponent<RoomType>().type != 1)
        {
            downDetection.GetComponent<EdgeCollider2D>().isTrigger = true;
        }

        if (rightDetection != null && rightDetection.GetComponent<RoomType>().type != 4)
        {
            rightDetection.GetComponent<EdgeCollider2D>().isTrigger = true;
        }

        if (leftDetection != null && leftDetection.GetComponent<RoomType>().type != 4)
        {
            leftDetection.GetComponent<EdgeCollider2D>().isTrigger = true;
        }



        //for (int i = 0; i < roomDetection.Count - 1; i++)
        //{
        //    if (roomDetection[i] != null && roomDetection[i].GetComponent<EdgeCollider2D>().isTrigger == false)
        //    {
        //        roomDetection[i].GetComponent<EdgeCollider2D>().isTrigger = true;
        //    }
        //}

    }


}
