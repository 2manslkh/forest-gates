using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform player;
    public GameObject fireballPrefab;
    public float fireballSpeed = 5;
    public Camera cam;
    private Vector2 mousePosition;
    private Rigidbody2D player_rb;


    // Start is called before the first frame update
    void Start()
    {
        player_rb = player.GetComponent<Rigidbody2D>();
    }
    public void ShootFireball()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootingDir = mousePosition - player_rb.position;
        float fireballAngle = Mathf.Atan2(shootingDir.y, shootingDir.x) * Mathf.Rad2Deg + 90;
        GameObject fireballInstance = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        fireballInstance.GetComponent<Rigidbody2D>().velocity = shootingDir.normalized * fireballSpeed;
        fireballInstance.transform.Rotate(0, 0, fireballAngle);
        Destroy(fireballInstance, 2.0f);
    }
}
