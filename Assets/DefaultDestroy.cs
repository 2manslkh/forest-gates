using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDestroy : MonoBehaviour
{
    public Vector3 moveVector;
    private SpriteRenderer sprite;
    private float disappearTimer;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    void PlaySoundBeforeDestroy(){
        gameObject.GetComponent<AudioSource>().Play();
    }
    void Destroy(){
        Destroy(gameObject);
    }

    void Start(){
        sprite = gameObject.GetComponent<SpriteRenderer>();
        disappearTimer = DISAPPEAR_TIMER_MAX;
        PlaySoundBeforeDestroy();
    }
    private void Update() {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 16f * Time.deltaTime;

        // if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {
        //     // First half of the popup lifetime
        //     float increaseScaleAmount = 1f;
        //     transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        // } else {
        //     // Second half of the popup lifetime
        //     float decreaseScaleAmount = 1f;
        //     transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        // }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            // Start disappearing
            float disappearSpeed = 3f;
            float new_a = sprite.color.a - disappearSpeed * Time.deltaTime;
            sprite.color = new Color(sprite.color.r,sprite.color.g,sprite.color.b,new_a);
            if (new_a < 0) {
                Destroy(gameObject);
            }
        }
    }
}
