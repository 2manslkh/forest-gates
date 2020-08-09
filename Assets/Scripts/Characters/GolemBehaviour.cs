using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehaviour : MonoBehaviour
{
    private Transform playerPos;
    public GameObject rockPrefab;
    private enum State {
        Follow,
        Attack,
        Slam,
    }
    private State state;
    private Animator animator;
    private float speed;
    public float attackDistance;
    private CharacterStats characterStats;
    private SpriteRenderer spriteRenderer;
    private bool Slamming;
    private Vector3 currentPatrolSpot;
    public float timeToSlam;
    private bool readySlam;
    private float rockSize;
    private bool doneAttacking;
    // Start is called before the first frame update
    private void Awake() {
        state = State.Follow;
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 0.7f;
        timeToSlam = 2;
        rockSize = 3f;
    }
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 difference = playerPos.position - animator.transform.position;
        switch (state) {
            default:
            case State.Follow:
                timeToSlam -= Time.deltaTime;
                if (timeToSlam < 0)
                {
                    state = State.Slam;
                    timeToSlam = 5;
                }

                animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
                animator.SetBool("isFollowing", true);
                animator.SetFloat("Horizontal", difference.x);
                animator.SetFloat("Vertical", difference.y);

                

                if (difference.magnitude <= attackDistance) {
                    state = State.Attack;
                    animator.SetBool("isFollowing", false);
                }
                break;
            case State.Attack:
                animator.SetBool("isAttacking", true);

                if(animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    animator.SetFloat("Horizontal", difference.x);
                    animator.SetFloat("Vertical", difference.y);
                }

                if (difference.magnitude > attackDistance) {
                    state = State.Follow;
                    animator.SetBool("isAttacking", false);
                }
                break;

            case State.Slam:
                if(!Slamming)
                {
                    Slamming = true;
                    readySlam = false;
                }
                animator.SetBool("isSlamming", true);
                var SlamTowards = Vector2.MoveTowards(animator.transform.position, currentPatrolSpot, 3.0f * Time.deltaTime);
                animator.SetFloat("Horizontal", SlamTowards.x);
                animator.SetFloat("Vertical", SlamTowards.y);
                StartCoroutine("Timer");
                if(readySlam)
                {
                    animator.transform.position = SlamTowards;
                }
                if(animator.transform.position == currentPatrolSpot)
                {
                    Slamming = false;
                    readySlam = false;
                    animator.SetBool("isSlamming", false);
                    state = State.Follow;
                }
                break;
        }
    }

    public void SlamRocks()
    {
        float mapHeight = Camera.main.orthographicSize * 2.0f;
        float mapWidth = mapHeight * Camera.main.aspect;
        for(int i=0; i < 8; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(0 + rockSize, mapWidth - rockSize), Random.Range(0 + rockSize, mapHeight - rockSize));
            var rock = Instantiate(rockPrefab, randomPosition, Quaternion.identity);
        }
        doneAttacking = true;
    }

}
