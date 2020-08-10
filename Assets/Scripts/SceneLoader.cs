using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y), Vector2.zero, 0);
        if (hit) {
            if (hit.collider.CompareTag("DialogueContinueButton")) 
            {
                Debug.Log("Player clicked continue button.");
                LoadNextScene();
            }
        }
    }

    public void LoadNextScene(){
       StartCoroutine(LoadDiffScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadDiffScene(int sceneBuildIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
