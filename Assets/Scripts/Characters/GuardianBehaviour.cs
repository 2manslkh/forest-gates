using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianBehaviour : MonoBehaviour
{
    private Transform playerPos;
    private enum State {
        Patrol,
        Attack,
        Slash,
    }
    public GameObject projectilePrefab;
    public GameObject tornadoPrefab;
    private State state;
    private Animator animator;
    private float speed;
    public float attackDistance;
    private CharacterStats characterStats;
    private SpriteRenderer spriteRenderer;
    public GameObject[] patrolSpots;
    private bool moving;
    private Vector3 currentPatrolSpot;
    private Vector3 lastPatrolSpot;
    private bool readyDash;
    private bool doneSlashing, doneAttacking;
    private int currentSpot;
    private int slashingTimes;
    private float projectileSpeed;
    private float numberOfProjectiles;
    private float radius;

    private void Awake() {
        state = State.Patrol;
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 3f;
        moving = false;
        projectileSpeed = 3.0f;
        numberOfProjectiles = 20;
        radius = 5.0f;
        slashingTimes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerPos)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
        Vector2 difference = playerPos.position - animator.transform.position;
        switch (state) {
            default:
            case State.Patrol:
                if(!moving)
                {
                    currentPatrolSpot = patrolSpots[Random.Range(0, patrolSpots.Length)].transform.position;
                    while(currentPatrolSpot == lastPatrolSpot)
                    {
                        currentPatrolSpot = patrolSpots[Random.Range(0, patrolSpots.Length)].transform.position;
                    }
                    lastPatrolSpot = currentPatrolSpot;
                    moving = true;
                }
                var moveDir = Vector2.MoveTowards(animator.transform.position, currentPatrolSpot, speed * Time.deltaTime);
                animator.SetFloat("Horizontal", moveDir.x);
                animator.SetFloat("Vertical", moveDir.y);
                animator.transform.position = moveDir;
                if(animator.transform.position == currentPatrolSpot)
                {
                    moving = false;
                    animator.SetBool("isPatrolling", false);
                    switch(Random.Range(0,2)){
                        default: state = State.Slash; break;
                        case 0: state = State.Slash; break;
                        case 1: state = State.Attack; break;
                    }
                }
                break;

            case State.Attack:
                if (doneAttacking)
                {
                    animator.SetBool("isAttacking", false);
                    if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        doneAttacking = false;
                        state = State.Patrol;
                        slashingTimes = 0;
                    }
                } else {
                    animator.SetFloat("Horizontal", difference.x);
                    animator.SetFloat("Vertical", difference.y);
                    animator.SetBool("isAttacking", true);
                }
                break;

            case State.Slash:
                if (doneSlashing)
                {
                    animator.SetBool("isSlashing", false);
                    if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
                    {
                        doneSlashing = false;
                        state = State.Patrol;
                        slashingTimes = 0;
                    }
                } else {
                    animator.SetFloat("Horizontal", difference.x);
                    animator.SetFloat("Vertical", difference.y);
                    animator.SetBool("isSlashing", true);
                }
                break;
        }
    }


    public void SpawnProjectiles()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = Random.Range(0, 180f);

        for(int i=0; i < numberOfProjectiles; i++)
        {
            float projectileXPosition = gameObject.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileYPosition = gameObject.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2 (projectileXPosition, projectileYPosition);
            Vector2 guardianPosition = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y);
            Vector2 projectileMoveDirection = (projectileVector - guardianPosition).normalized * projectileSpeed;
            var projectile = Instantiate(projectilePrefab, guardianPosition, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            angle += angleStep;
        }

        slashingTimes += 1;
        if (slashingTimes >= 3){
            doneSlashing = true;
        }
    }

    public void SpawnTornados()
    {
        float angle = Random.Range(0, 360f);
        float tornadoXPosition = gameObject.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
        float tornadoYPosition = gameObject.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

        Vector2 tornadoVector = new Vector2 (tornadoXPosition, tornadoYPosition);
        Vector2 guardianPosition = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y);
        // Vector2 tornadoMoveDirection = (tornadoVector - guardianPosition).normalized * projectileSpeed;
        Vector2 difference = playerPos.position - animator.transform.position;
        Vector2 tornadoMoveDirection = difference.normalized * projectileSpeed;
        for(int i=0; i < 8; i++)
        {
            float randomnessX = Random.Range(-2f, 2f);
            float randomnessY = Random.Range(-2f, 2f);
            var tornado = Instantiate(tornadoPrefab, guardianPosition, Quaternion.identity);
            tornado.GetComponent<Rigidbody2D>().velocity = new Vector2(tornadoMoveDirection.x + randomnessX, tornadoMoveDirection.y + randomnessY);
        }
        doneAttacking = true;
    }

}
