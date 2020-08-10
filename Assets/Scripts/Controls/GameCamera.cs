using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public static GameCamera instance;

    public GameObject CurrentRoom;

    Vector3 targetPosition = new Vector3();

    public float targetAspect;

    public float fov;

    private Camera cam;

    void Awake()
    {   
        instance = this;
        cam = Camera.main;
        // set fov == 90 for boss room
        // set fov == 53 for normal level
        cam.fieldOfView = fov;
        cam.orthographic = false;
        //Camera.main.fieldOfView = fov;
        //Camera.main.orthographic = false;
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
            targetPosition.x = GameObject.FindWithTag("Player").transform.position.x;
            targetPosition.y = GameObject.FindWithTag("Player").transform.position.y;
        }
        // allow the camera to follow the player around in boss rooms
        else
        {
            targetPosition.x = CurrentRoom.transform.position.x;
            targetPosition.y = CurrentRoom.transform.position.y;

        }
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
        // targetAspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;
        //float windowaspect = screenWidth / screenHeight;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetAspect;

        // obtain camera component so we can modify its viewport
        // Camera camera = GetComponent<Camera>();
        cam.fieldOfView = fov;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            cam.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = cam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
