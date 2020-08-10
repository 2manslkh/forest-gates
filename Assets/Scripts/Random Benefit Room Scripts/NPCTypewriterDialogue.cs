using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTypewriterDialogue : MonoBehaviour
{
    NPCDialogueTrigger FairyTriggered;
    NPCDialogueTrigger MinotaurTriggered;
    NPCDialogueTrigger OrcTriggered;
    public float delay = 0.1f;
    public string MinotaurText;
    public string FairyText;
    public string OrcText;

    private string currentText = "";
     
     void Start(){
        FairyTriggered = GameObject.Find("Fairy").GetComponent<NPCDialogueTrigger>();
        MinotaurTriggered = GameObject.Find("Minotaur").GetComponent<NPCDialogueTrigger>();
        OrcTriggered = GameObject.Find("Orc").GetComponent<NPCDialogueTrigger>();
     }

     void Update(){
         if (FairyTriggered){
            StartCoroutine(ShowTextFairy());
         }
         if (OrcTriggered){
             StartCoroutine(ShowTextOrc());
         }
         if (MinotaurTriggered){
             StartCoroutine(ShowTextOrc());
         }
         
     }

     IEnumerator ShowTextFairy(){
        for (int i = 0; i < FairyText.Length; i++){
            currentText = FairyText.Substring(0,i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
     IEnumerator ShowTextOrc(){
        for (int i = 0; i < OrcText.Length; i++){
            currentText = OrcText.Substring(0,i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
     IEnumerator ShowTextMinotaur(){
        for (int i = 0; i < MinotaurText.Length; i++){
            currentText = MinotaurText.Substring(0,i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
