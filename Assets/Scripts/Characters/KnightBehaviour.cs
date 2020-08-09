using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehaviour : MonoBehaviour
{
    private Transform playerPos;
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
    // Start is called before the first frame update
    private void Awake() {
        state = State.Follow;
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 0.7f;
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
                    print("BANKAII");
                    Bankai();
                }

                if(animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    animator.SetFloat("Horizontal", difference.x);
                    animator.SetFloat("Vertical", difference.y);
                }

                if (difference.magnitude > attackDistance) {
                    state = State.Dash;
                    animator.SetBool("isAttacking", false);
                }
                break;

            case State.Dash:
                if(!dashing)
                {
                    currentPatrolSpot = patrolSpots[Random.Range(0, patrolSpots.Length)].transform.position;
                    dashing = true;
                }
                var dashTowards = Vector2.MoveTowards(animator.transform.position, currentPatrolSpot, 3.0f * Time.deltaTime);
                animator.transform.position = dashTowards;
                animator.SetFloat("Horizontal", dashTowards.x);
                animator.SetFloat("Vertical", dashTowards.y);
                if(animator.transform.position == currentPatrolSpot)
                {
                    dashing = false;
                    state = State.Follow;
                }
                print(animator.transform.position);
                print(currentPatrolSpot);
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
        // gameObject.transform.position = patrolSpots[Random.Range(0, patrolSpots.Length)].transform.position;
    }
}
