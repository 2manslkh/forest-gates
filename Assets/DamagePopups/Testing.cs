﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Testing : MonoBehaviour {

    
    [SerializeField] private Transform pfDamagePopup;
    private void Start() {
        Instantiate(GameAssets.i.pfDamagePopup, Vector3.zero, Quaternion.identity);
        // DamagePopup.Create(Vector3.zero, 300);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // bool isCriticalHit = Random.Range(0, 100) < 30;
            DamagePopup.Create(Input.mousePosition, 100, "good");
        }
    }
}
