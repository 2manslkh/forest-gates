using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePopupTown : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject toContinueCanvas;

    private bool finishedTalking;
    private float sec = 3f;

    
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(true);
        toContinueCanvas.SetActive(false);
        finishedTalking = false;
        StartCoroutine(LateCall());
        
    }

    // Update is called once per frame
    void Update()
    {
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
