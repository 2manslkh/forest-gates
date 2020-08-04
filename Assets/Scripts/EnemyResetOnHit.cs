using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResetOnHit : MonoBehaviour
{
    void resetHit(){
        gameObject.GetComponent<Enemy>().isHit = false;
        Debug.Log(gameObject.GetComponent<Enemy>().isHit);
    }
}
