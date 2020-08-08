using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmoEnemy : MonoBehaviour
{
    public GameObject enemy;
    private float radius;
    void Start(){
        radius = enemy.GetComponent<Enemy>().attackRadius;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }
}
