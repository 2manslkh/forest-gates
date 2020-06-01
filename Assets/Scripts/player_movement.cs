using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    // Start is called before the first frame update
   
    public float speed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator anim;
    private Vector2 movement;
    private Vector2 mousePosition;
    // Update is called once per frame
    void Update()
    {
        // Get movement of wasd keys
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

        anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal")); // using raw to change animation immediately
        anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("Magnitude", movement.magnitude);

        //mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = transform.position + movement * Time.deltaTime;
    }

    void FixedUpdate()
    {
        //update player position
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        // Get vector that points from player to mouse position
        //Vector2 lookDir = mousePosition - rb.position;
        // Get angle of vector
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //update player rotation
        //rb.rotation = angle;
    }
}