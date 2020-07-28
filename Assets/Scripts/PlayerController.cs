using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2D;
    private Vector2 moveVelocity;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + moveVelocity * Time.fixedDeltaTime);
    }
}