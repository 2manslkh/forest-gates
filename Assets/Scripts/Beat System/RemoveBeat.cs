using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBeat : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("col");
        if (col.gameObject.tag == "Beat"){
            Destroy(col.gameObject);
        }
    }
}
