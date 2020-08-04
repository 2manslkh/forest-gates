using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVisible : MonoBehaviour
{
    Animator animator;
    Renderer spriteRenderer;
    private Camera camera;
    // Start is called before the first frame update
    private void Start() {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<Renderer>();
        camera = Camera.main;
    }
    private void Update() {
        if (camera.tag == "MainCamera")
        {
            if (spriteRenderer.isVisible == false)
            {
                animator.enabled = false;
            }
            else
            {
                animator.enabled = true;
            }
        }
        
    }
    void OnBecameInvisible()
    {
        animator.enabled = false;
    }

    // ...and enable it again when it becomes visible.
    void OnBecameVisible()
    {
        animator.enabled = true;

    }
}
