using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
  Wander,
  Follow,
  Attack,
  Die,
};

public class Enemy : MonoBehaviour
{
    GameObject player;
    public EnemyState currState = EnemyState.Wander;
    public Transform target;
    Rigidbody2D rb;
    public float detectionRange = 5f;
    public float attackRange = 0.8f;
    public float moveSpeed = 2f;
    public Animator anim;
    private Vector2 moveDirection;
    private int moveDir;
    private bool canChangeDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        canChangeDir = true;
        moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):
                break;
        }

        if(IsPlayerInRange(attackRange) && currState != EnemyState.Die)
        {
            currState = EnemyState.Attack;
        }
        else if(IsPlayerInRange(detectionRange) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if(!IsPlayerInRange(detectionRange)&& currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }
    }

    private bool IsPlayerInRange(float detectionRange)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= detectionRange;
    }

    void Wander()
    {
        anim.SetBool("Moving", true);
        MovementHandler();

    }

    void Follow()
    {
        anim.SetBool("Moving", true);
        transform.position += (player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
    }

    void Attack()
    {
        anim.SetBool("Moving", false);
        anim.SetBool("Attacking", true);
        rb.velocity = Vector2.zero;
        // StartCoroutine("Attack")
    }

    void ChangeDirection()
    {
        // Can move in any direction

        moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
        if (moveDir < 1 || moveDir > 4)
        {
            while (moveDir < 1 || moveDir > 4)
            {
                moveDir = Random.Range(1, 5);
            }
        }
        switch (moveDir)
        {
            case (1):
                anim.SetFloat("Vertical", -1);
                anim.SetFloat("Horizontal", 0);
                rb.velocity = Vector2.down * moveSpeed;
                break;
            case (2):
                anim.SetFloat("Vertical", 0);
                anim.SetFloat("Horizontal", -1);
                rb.velocity = Vector2.left * moveSpeed;
                break;
            case (3):
                anim.SetFloat("Vertical", 0);
                anim.SetFloat("Horizontal", 1);
                rb.velocity = Vector2.right * moveSpeed;
                break;
            case(4):
                anim.SetFloat("Vertical", 1);
                anim.SetFloat("Horizontal", 0);
                rb.velocity = Vector2.up * moveSpeed;
                break;
        }
    }
 

    void MovementHandler()
    {
        if (canChangeDir == true)
        {
            StartCoroutine(RandomMovement());
        }
    }

    IEnumerator RandomMovement()
    {
        canChangeDir = false;
        ChangeDirection();
        yield return new WaitForSeconds(0.8f);
        canChangeDir = true;
    }
}
