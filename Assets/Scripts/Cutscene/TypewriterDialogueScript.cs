﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TypewriterDialogueScript : MonoBehaviour
{
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowText(){
        for (int i = 0; i < fullText.Length; i++){
            currentText = fullText.Substring(0,i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
