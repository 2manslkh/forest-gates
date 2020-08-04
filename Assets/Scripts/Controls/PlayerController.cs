﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, BasicAttackInterface
{
    // Start is called before the first frame update
   
    public float speed = 5f;
    private Rigidbody2D rb;
    private Camera cam;
    public Animator anim;
    public Animator weapon_anim;
    private Vector3 movement;
    private Vector2 mousePosition;
    // Update is called once per frame
    private bool isAttacking;
    public Transform attackPosition;
    public LayerMask enemyLayer;
    public float attackRadius;
    private int damage;


    public enum facingDirection{ //WIP
        UP=1,
        RIGHT=2,
        DOWN=3,
        LEFT=4
    } 

    private void Start() {
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();
        // anim = gameObject.GetComponent<Animator>();
        anim.enabled = false;
        anim.enabled = true;
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
    void Update()
    {
        // Time.timeScale = 0.05f;
        // Get input of WASD keys
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Get mouse position
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            mousePosition = cam.ScreenToWorldPoint(mousePos);
            // Get vector that points from player to mouse position
            Vector2 lookDir = mousePosition - rb.position;
            anim.SetFloat("Mouse X", lookDir.x);
            anim.SetFloat("Mouse Y", lookDir.y);
            anim.SetFloat("Magnitude", movement.magnitude);
            weapon_anim.SetFloat("Mouse X", lookDir.x);
            weapon_anim.SetFloat("Mouse Y", lookDir.y);
            weapon_anim.SetFloat("Magnitude", movement.magnitude);
            transform.position = transform.position + movement * Time.deltaTime;
            
            attackPosition.position = Vector3.Normalize(lookDir) + gameObject.transform.position;
        }

        if (Input.GetMouseButtonDown(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
            StartCoroutine(animateBasicAttack(anim));

            anim.SetBool("BasicAttack", true);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, enemyLayer);
            Debug.Log(enemiesToDamage.Length);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                damage = gameObject.GetComponent<PlayerStats>().damage.GetValue(); // Calculate damage with equipement modifiers
                Debug.Log(enemiesToDamage[i]);
                enemiesToDamage[i].GetComponent<CharacterStats>().TakeDamage(damage);
                if (!enemiesToDamage[i].GetComponent<Animator>().GetBool("isHit")){
                    enemiesToDamage[i].GetComponent<Animator>().SetBool("isHit", true);
                }
            }
        }
    }
      void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    // Basic Attack Animation Routine
    public IEnumerator animateBasicAttack(Animator anim){
        anim.SetBool("BasicAttack", true);
        weapon_anim.SetBool("BasicAttack", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("BasicAttack", false);
        weapon_anim.SetBool("BasicAttack", false);
    }
}