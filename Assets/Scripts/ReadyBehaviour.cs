using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyBehaviour : StateMachineBehaviour
{
    // public GameObject effect;
		private Transform playerPos;
		public float followDistance;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.enabled = true;
		playerPos = GameObject.FindGameObjectWithTag("Player").transform;
	}


	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Vector2 difference = playerPos.position - animator.transform.position;
		if (difference.magnitude > followDistance) {
			animator.SetBool("isFollowing", false);
		}
	}
}
