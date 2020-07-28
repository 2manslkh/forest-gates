using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    private PatrolSpots patrol;
    public float speed;
    private int randomSpot;
    private Transform playerPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol = GameObject.FindGameObjectWithTag("PatrolSpots").GetComponent<PatrolSpots>();
        randomSpot = Random.Range(0, patrol.patrolPoints.Length);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 difference = patrol.patrolPoints[randomSpot].position - animator.transform.position;
        if (Vector2.Distance(animator.transform.position, patrol.patrolPoints[randomSpot].position) > 0.2f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrol.patrolPoints[randomSpot].position, speed * Time.deltaTime);
            animator.SetFloat("Horizontal", difference.x);
            animator.SetFloat("Vertical", difference.y);

        }
        else
        {
            randomSpot = Random.Range(0, patrol.patrolPoints.Length);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            animator.SetBool("isPatrolling", false);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetBool("isFollowing", true);
        }

        if (difference.magnitude < 2.0f) {
            animator.SetBool("isAttacking", true);
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
