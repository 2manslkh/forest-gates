﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public static GameCamera Instance;

    public GameObject CurrentRoom;

    public float MovementSpeedOnRoomChange;

    Vector3 targetPosition = new Vector3();

    public float targetaspect;

    public float fov;

    void Awake()
    {
        Instance = this;
        Camera camera = GetComponent<Camera>();
        camera.fieldOfView = fov;
    }

    // Start is called before the first frame update
    void Start()
    {
        screenSize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (CurrentRoom == null)
        {
            return;
        }
        targetPosition.x = CurrentRoom.transform.position.x;
        targetPosition.y = CurrentRoom.transform.position.y;
        targetPosition.z = transform.position.z;

        //gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * MovementSpeedOnRoomChange);
        gameObject.transform.position = targetPosition;
    }

    void screenSize()
    {
        //Screen.SetResolution(screenWidth, screenHeight, false);
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        //targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;
        //float windowaspect = screenWidth / screenHeight;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();
        camera.fieldOfView = fov;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
