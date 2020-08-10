using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWallBehaviour : MonoBehaviour
{
    public int damage;
    private bool hitOnce;
    private Animator animator;
    public float horizontal;
    public float vertical;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        Destroy(gameObject, 3.0f);
        damage = 20;
        hitOnce = false;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player" && !hitOnce)
        {
            DamagePopup.Create(other.transform.position, damage);
            if (!other.gameObject.GetComponent<Animator>().GetBool("isHit")){
                    other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
                    other.gameObject.GetComponent<Player>().setHit();
                }
            hitOnce = true;
        }
    }

    public void UpdateAnimator(float horizontal, float vertical)
    {
        this.animator.SetFloat("Horizontal", horizontal);
        this.animator.SetFloat("Vertical", vertical);
    }
}
