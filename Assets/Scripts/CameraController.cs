using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    private Animator animator;
    public bool followCharacter;
    public float speed = 2.0f;
    
    void Start(){
        animator = gameObject.GetComponent<Animator>();
    }
    void Update () {
        float interpolation = speed * Time.deltaTime;
        
        Vector3 position = this.transform.position;
        if (followCharacter){
            position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
            position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
            
            this.transform.position = position;
        }
    }
    public void playShake(string strength){
        if (strength == "perfect"){
            animator.SetBool("criticalHit",true);
        } else if (strength == "great"){
            animator.SetBool("greatHit",true);
        }
    }

    public void ResetState(){
        animator.SetBool("greatHit", false);
        animator.SetBool("criticalHit", false);
    }
}
