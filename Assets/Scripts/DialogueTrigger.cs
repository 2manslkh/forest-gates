using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //public Dialogue dialogue;
    //public GameObject dialogueBox;
    public Animation anim;

    void OnAwake(){
        anim = GetComponent<Animation>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if(collider.gameObject.CompareTag("Gatekeeper")){
            Debug.Log(collider.tag);
            if (Input.GetKeyDown("E")){
                print("E key was pressed");
                anim.Play();
            }
        }
    }
}
