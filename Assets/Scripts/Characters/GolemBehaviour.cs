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
        Smash,
    }
    private State state;
    private Animator animator;
    private float speed;
    public float attackDistance;
    private CharacterStats characterStats;
    private SpriteRenderer spriteRenderer;
    private bool Smashing;
    private Vector3 currentPatrolSpot;
    public float timeToSmash;
    private bool readySmash;
    private float rockSize;
    private bool doneAttacking;
    // Start is called before the first frame update
    private void Awake() {
        state = State.Follow;
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 0.7f;
        timeToSmash = 2;
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
                timeToSmash -= Time.deltaTime;
                if (timeToSmash < 0)
                {
                    state = State.Smash;
                    timeToSmash = 5;
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

            case State.Smash:
                if(!Smashing)
                {
                    Smashing = true;
                    readySmash = false;
                }
                animator.SetBool("isSmashing", true);
                var SmashTowards = Vector2.MoveTowards(animator.transform.position, currentPatrolSpot, 3.0f * Time.deltaTime);
                animator.SetFloat("Horizontal", SmashTowards.x);
                animator.SetFloat("Vertical", SmashTowards.y);
                StartCoroutine("Timer");
                if(readySmash)
                {
                    animator.transform.position = SmashTowards;
                }
                if(animator.transform.position == currentPatrolSpot)
                {
                    Smashing = false;
                    readySmash = false;
                    animator.SetBool("isSmashing", false);
                    state = State.Follow;
                }
                break;
        }
    }

    public void SmashRocks()
    {
        float mapHeight = Camera.main.orthographicSize * 2.0f;
        float mapWidth = mapHeight * Camera.main.aspect;
        for(int i=0; i < 8; i++)
        {
            Vector2 randomPosition = (Random.Range(0 + rockSize, mapWidth - rockSize), Random.Range(0 + rockSize, mapHeight - rockSize);
            var rock = Instantiate(rockPrefab, randomPosition, Quaternion.identity);
        }
        doneAttacking = true;
    }

}
