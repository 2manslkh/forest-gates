using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    public int damage;
    private bool hitOnce;
    private void Start() {
        Destroy(gameObject, 2.0f);
        damage = 20;
        hitOnce = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !hitOnce)
        {
            DamagePopup.Create(other.transform.position, damage);
            if (!other.gameObject.GetComponent<Animator>().GetBool("isHit")){
                    other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
                    other.gameObject.GetComponent<Player>().setHit();
                }
            hitOnce = true;
        }
    }
}
