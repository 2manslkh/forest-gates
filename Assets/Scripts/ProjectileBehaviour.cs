using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start() {
        Destroy(this, 3);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            // Player takes dmg here
            // other.GetComponent
        }
    }
}
