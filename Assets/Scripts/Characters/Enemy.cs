using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    public Rigidbody2D rb;
    public Animator enemyAnimator;
    public bool isHit = false;

    void resetHit(){
    //    enemyAnimator.SetBool("isHit",false);
    }
}
