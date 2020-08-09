using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoBehaviour : MonoBehaviour
{
    public int damage;
    private bool hitOnce;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);
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
