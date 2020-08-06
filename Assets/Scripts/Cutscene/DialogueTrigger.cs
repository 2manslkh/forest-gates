using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //public Dialogue dialogue;
    public GameObject dialogueBox;
    public GameObject toContinueCanvas;
    private bool triggered;
    private bool finishedTalking;
    private float sec = 7f;
    

    void OnAwake(){
        dialogueBox.SetActive(false);
        toContinueCanvas.SetActive(false);
        triggered = false;
        finishedTalking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gatekeeper"))
        {
            triggered = true;
            Debug.Log(other.tag);
        }

    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E) && triggered)
        {
            dialogueBox.SetActive(true);
            Debug.Log("E key was pressed");
            StartCoroutine(LateCall());
        }

        if (Input.GetKeyDown(KeyCode.Return) && finishedTalking){
            dialogueBox.SetActive(false);
            toContinueCanvas.SetActive(false);
            Debug.Log("Enter was pressed after talking");
        }
        
    }

    IEnumerator LateCall()
     {
         yield return new WaitForSeconds(sec);
         toContinueCanvas.SetActive(true);
         finishedTalking = true;
         Debug.Log("finishedTalking is true");
     }
    
}
