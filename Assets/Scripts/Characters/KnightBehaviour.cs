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
    public float timeToDash;
    private bool readyDash, stopDash;
    private float dashSpeed;
    private bool isTeleporting;
    private float opacity;
    private bool hitOnce;
    private int currentPatrolIndex;
    private AudioSource[] audioClips;
    // Start is called before the first frame update
    private void Awake() {
        state = State.Follow;
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioClips = GetComponents<AudioSource>();
        speed = 3f;
        timeToDash = 5;
        dashSpeed = 20f;
        attackDistance = 3f;
        opacity = 255f;
        hitOnce = false;
        currentPatrolIndex = 0;
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
            case State.Follow:
                timeToDash -= Time.deltaTime;
                if (timeToDash < 0 && !isTeleporting)
                {
                    state = State.Dash;
                    timeToDash = 8;
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
                print(isTeleporting);
                if(!isTeleporting){
                    animator.SetBool("isAttacking", true);
                    opacity = 255f;
                } else {
                    animator.speed = 0;
                    animator.SetBool("isAttacking", false);
                    opacity -= Time.deltaTime*300;
                }
                spriteRenderer.color = new Color(255, 255, 255, opacity/255f);

                if(animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !isTeleporting)
                {
                    animator.SetFloat("Horizontal", difference.x);
                    animator.SetFloat("Vertical", difference.y);

                    if (difference.magnitude > attackDistance) {
                        state = State.Follow;
                        animator.SetBool("isAttacking", false);
                    }
                }
                break;

            case State.Dash:
                if(!dashing)
                {
                    currentPatrolIndex = 0;
                    // currentPatrolSpot = patrolSpots[Random.Range(0, patrolSpots.Length)].transform.position;
                    RandomizeArray(patrolSpots);
                    stopDash = false;
                    dashing = true;
                    readyDash = false;
                }
                animator.SetBool("isDashing", true);
                StartCoroutine("Timer");
                if(readyDash && dashing)
                {
                    currentPatrolSpot = patrolSpots[currentPatrolIndex].transform.position;
                    Vector2 dashTowards = Vector2.MoveTowards(animator.transform.position, currentPatrolSpot, dashSpeed * Time.deltaTime);
                    animator.SetFloat("Horizontal", dashTowards.x);
                    animator.SetFloat("Vertical", dashTowards.y);
                    animator.transform.position = dashTowards;
                    if(animator.transform.position == currentPatrolSpot)
                    {
                        audioClips[1].Play();
                        currentPatrolIndex += 1;
                    }
                    if(currentPatrolIndex == patrolSpots.Length)
                    {
                        stopDash = true;
                    }
                }
                if(stopDash)
                {
                    dashing = false;
                    readyDash = false;
                    animator.SetBool("isDashing", false);
                    hitOnce = false;
                    state = State.Follow;
                    stopDash = false;
                }
                break;
        }
    }

    public void Teleport()
    {
        if(!isTeleporting){
            isTeleporting = true;
            audioClips[2].Play();
            StartCoroutine("TeleportTimer");
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        readyDash = true;
    }

    IEnumerator TeleportTimer()
    {
        yield return new WaitForSeconds(0.8f);
        animator.speed = 1;
        gameObject.transform.position = patrolSpots[Random.Range(0, patrolSpots.Length)].transform.position;
        isTeleporting = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player" && !hitOnce && state==State.Dash)
        {
            DamagePopup.Create(other.transform.position, characterStats.damage.GetValue());
            if (!other.gameObject.GetComponent<Animator>().GetBool("isHit")){
                    other.gameObject.GetComponent<CharacterStats>().TakeDamage(characterStats.damage.GetValue());
                    other.gameObject.GetComponent<Player>().setHit();
                }
            hitOnce = true;
        }
    }


    private void RandomizeArray(GameObject[] arr)
    {
        for (var i = arr.Length - 1; i > 0; i--) {
            var r = Random.Range(0,i);
            var tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
}
