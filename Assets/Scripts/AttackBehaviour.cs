using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    private Transform playerPos;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
	}


	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Vector2 difference = playerPos.position - animator.transform.position;
        animator.SetFloat("Horizontal", difference.x);
        animator.SetFloat("Vertical", difference.y);

        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetBool("isFollowing", true);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            animator.SetBool("isPatrolling", true);
        }
        if (difference.magnitude > 1.5f) {
            animator.SetBool("isAttacking", false);
        }
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
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
