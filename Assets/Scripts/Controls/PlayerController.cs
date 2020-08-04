using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
   
    public float speed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator anim;
    private Vector3 movement;
    private Vector2 mousePosition;
    // Update is called once per frame

    public enum facingDirection{ //WIP
        UP=1,
        RIGHT=2,
        DOWN=3,
        LEFT=4
    } 

    public int currentFacingDirection;

    void updateDirection(float angle){ //WIP
        if(0 <= angle && angle < 90){
            currentFacingDirection = (int) facingDirection.UP;
        } else if(90 <= angle && angle < 180){
            currentFacingDirection = (int) facingDirection.RIGHT;
        } else if(180 <= angle && angle < 270){
            currentFacingDirection = (int) facingDirection.DOWN;
        } else if(270 <= angle && angle < 180){
            currentFacingDirection = (int) facingDirection.LEFT;
        }
    }

    private bool isAttacking;
    public Transform attackPosition;
    public LayerMask enemyLayer;
    public float attackRadius;
    private int damage;

    void Update()
    {
        isAttacking = anim.GetBool("BasicAttack");

        // Get input of WASD keys
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Get mouse position
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        // Get vector that points from player to mouse position
        Vector2 lookDir = mousePosition - rb.position;

        anim.SetFloat("Mouse X", lookDir.x);
        anim.SetFloat("Mouse Y", lookDir.y);
        anim.SetFloat("Magnitude", movement.magnitude);
        transform.position = transform.position + movement * Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && !isAttacking){
            anim.SetBool("BasicAttack", true);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, enemyLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                damage = gameObject.GetComponent<PlayerStats>().damage.GetValue(); // Calculate damage with equipement modifiers
                enemiesToDamage[i].GetComponent<CharacterStats>().TakeDamage(damage);
                if (!enemiesToDamage[i].GetComponent<Animator>().GetBool("isHit")){
                    enemiesToDamage[i].GetComponent<Animator>().SetBool("isHit", true);
                }
            }
        }
    }

    void resetBasicAttack(){
        anim.SetBool("BasicAttack", false);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }
}