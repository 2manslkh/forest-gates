using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator enemyAnimator;
    public bool isHit = false;

    // private void OnTriggerStay2D(Collider2D other) {
    //     Debug.Log("Hit");
    //     if (!isHit){
    //         enemyAnimator.SetTrigger("Hit");
    //         isHit = true;
    //         Debug.Log(isHit);
    //     }
    // }

    void resetHit(){
       enemyAnimator.SetBool("isHit",false);
    }
}
