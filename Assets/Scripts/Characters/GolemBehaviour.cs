using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehaviour : MonoBehaviour
{
    private Transform playerPos;
    public GameObject rockSpawnPrefab;
    public GameObject rockWallPrefab;

    private Camera cam;
    private enum State {
        Follow,
        Wall,
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
    public float timeToAttack;
    private bool readySlam;
    private float rockSize;
    private bool doneSlamming;
    private bool doneWalling;
    public float awayDistance;
    // Start is called before the first frame update
    private void Awake() {
        state = State.Follow;
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 2f;
        timeToSlam = 5;
        timeToSlam = 6;
        rockSize = 3f;
        cam = Camera.main;
        awayDistance = 8f;
        attackDistance = 3f;
    }
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 difference = playerPos.position - animator.transform.position;
        print(difference.normalized);

        switch (state) {
            default:
            case State.Follow:
                timeToSlam -= Time.deltaTime;
                if (timeToSlam < 0)
                {
                    animator.SetBool("isFollowing", false);
                    state = State.Slam;
                    timeToSlam = 5;
                }

                if (difference.magnitude < awayDistance) {
                    animator.transform.position = Vector2.MoveTowards(animator.transform.position, -difference.normalized* 10, speed * Time.deltaTime);
		        }
                animator.SetBool("isFollowing", true);
                animator.SetFloat("Horizontal", difference.x);
                animator.SetFloat("Vertical", difference.y);
                

                if (difference.magnitude <= attackDistance || difference.magnitude >= awayDistance) {
                    timeToAttack -= Time.deltaTime;
                    if (timeToAttack < 0)
                    {
                        print("attacking");
                        state = State.Wall;
                        animator.SetBool("isFollowing", false);
                        timeToAttack = 6;
                    }
                }
                break;
            case State.Wall:
                if (doneWalling)
                {
                    animator.SetBool("isAttacking", false);
                    if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        doneWalling = false;
                        state = State.Follow;
                    }
                } else {
                    animator.SetFloat("Horizontal", difference.x);
                    animator.SetFloat("Vertical", difference.y);
                    animator.SetBool("isAttacking", true);
                }
                break;

            case State.Slam:
                if (doneSlamming)
                {
                    animator.SetBool("isSlamming", false);
                    if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Slam"))
                    {
                        doneSlamming = false;
                        state = State.Follow;
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


    public void SpawnWall()
    {
        Vector2 difference = playerPos.position - animator.transform.position;
        Vector2 normalized = difference.normalized;
        Vector3 offset = Vector3.zero;
        if(normalized.x > 0 && (Mathf.Abs(normalized.x) > Mathf.Abs(normalized.y)))
        {
            //right
            offset = animator.transform.right*4;
        } else if (normalized.x < 0 && (Mathf.Abs(normalized.x) > Mathf.Abs(normalized.y)))
        {
            //left
            offset = -animator.transform.right*4;
        } else if (normalized.y > 0 && (Mathf.Abs(normalized.y) > Mathf.Abs(normalized.x)))
        {
            //up
            offset = animator.transform.up*4;
        } else if (normalized.y < 0 && (Mathf.Abs(normalized.y) > Mathf.Abs(normalized.x)))
        {
            //down
            offset = -animator.transform.up*4;
        }
        print(offset);
        Vector3 spawnPosition = new Vector3(animator.transform.position.x + normalized.x, animator.transform.position.y + normalized.y, animator.transform.position.z);
        var rockWall = Instantiate(rockWallPrefab, spawnPosition + offset, Quaternion.identity);
        print(animator.transform.right * 10);
        rockWall.GetComponent<RockWallBehaviour>().UpdateAnimator(difference.x, difference.y);
        doneWalling = true;
    }

}
