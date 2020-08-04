using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
public float pickupRadius = 1f; //pickup radius


    // This method is meant to be overwritten
	public virtual void Interact ()
	{
		
	}
    
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }

    void Start() {
        gameObject.GetComponent<CircleCollider2D>().radius = pickupRadius;
    }
}
