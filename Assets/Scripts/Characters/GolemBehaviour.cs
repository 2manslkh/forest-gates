using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehaviour : MonoBehaviour
{
    private Transform playerPos;
    public GameObject rockSpawnPrefab;
    private Camera cam;
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
    private bool doneSlamming;
    // Start is called before the first frame update
    private void Awake() {
        state = State.Follow;
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 0.7f;
        timeToSlam = 2;
        rockSize = 3f;
        cam = Camera.main;
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
                if (doneSlamming)
                {
                    animator.SetBool("isSlamming", false);
                    if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Slam"))
                    {
                        doneSlamming = false;
                        state = State.Attack;
                    }
                } else {
                    animator.SetFloat("Horizontal", difference.x);
                    animator.SetFloat("Vertical", difference.y);
                    animator.SetBool("isSlamming", true);
                }
                break;
        }
    }

    public void SlamRocks()
    {
        float minWidth = cam.ScreenToWorldPoint(new Vector3(0,0,0)).x;
        float maxWidth = cam.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x;
        float minHeight = cam.ScreenToWorldPoint(new Vector3(0,0,0)).y;
        float maxHeight = cam.ScreenToWorldPoint(new Vector3(0,Screen.height,0)).y;
        for(int i=0; i < 8; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minWidth, maxWidth), Random.Range(minHeight, maxHeight));
            var rock = Instantiate(rockSpawnPrefab, randomPosition, Quaternion.identity);
        }
        doneSlamming = true;
    }

}
