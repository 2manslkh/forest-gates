using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButton : MonoBehaviour
{
void Start() { }
void Update() { }

public void button1()
{
    Debug.Log("Button1");
    SceneManager.LoadScene("Scene1n2");
}

public void button2()
{
    Debug.Log("Button2");
    SceneManager.LoadScene("1_Bedroom");
}

}
