using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    public Rigidbody2D rb;
    public Animator enemyAnimator;
    public bool isHit = false;


    public void getHit(){
        enemyAnimator.SetBool("isHit",true);
    }

    public void resetHit(){
       enemyAnimator.SetBool("isHit",false);
    }
}
