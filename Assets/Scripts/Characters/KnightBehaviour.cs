using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehaviour : MonoBehaviour
{
    private Transform playerPos;
    private GameObject player;
    private enum State {
        Follow,
        Attack,
        Dash,
    }
    private State state;
    private Animator animator;
    private float speed;
    public float attackDistance;
    private CharacterStats characterStats;
    private SpriteRenderer spriteRenderer;
    public GameObject[] patrolSpots;
    private bool dashing;
    private Vector3 currentPatrolSpot;
    public float timeToDash;
    private bool readyDash;
    // Start is called before the first frame update
    private void Awake() {
        state = State.Follow;
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 0.7f;
        timeToDash = 2;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform;
        Vector2 difference = playerPos.position - animator.transform.position;
        switch (state) {
            default:
            case State.Follow:
                timeToDash -= Time.deltaTime;
                if (timeToDash < 0)
                {
                    state = State.Dash;
                    timeToDash = 5;
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

                if(characterStats.currentHealth < (characterStats.maxHealth.GetValue() / 2))
                {
                    Bankai();
                }

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

            case State.Dash:
                if(!dashing)
                {
                    currentPatrolSpot = patrolSpots[Random.Range(0, patrolSpots.Length)].transform.position;
                    dashing = true;
                    readyDash = false;
                }
                animator.SetBool("isDashing", true);
                var dashTowards = Vector2.MoveTowards(animator.transform.position, currentPatrolSpot, 3.0f * Time.deltaTime);
                animator.SetFloat("Horizontal", dashTowards.x);
                animator.SetFloat("Vertical", dashTowards.y);
                StartCoroutine("Timer");
                if(readyDash)
                {
                    animator.transform.position = dashTowards;
                }
                if(animator.transform.position == currentPatrolSpot)
                {
                    dashing = false;
                    readyDash = false;
                    animator.SetBool("isDashing", false);
                    state = State.Follow;
                }
                break;
        }
    }

    private void Bankai()
    {
        speed = 1.2f;
        animator.speed = 2.0f;
        spriteRenderer.color = new Color(119f/255f, 190f/255f, 49f/255f, 255f);
    }

    public void Teleport()
    {
        gameObject.transform.position = patrolSpots[Random.Range(0, patrolSpots.Length)].transform.position;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        readyDash = true;
    }
}
