using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    public GameObject shopUI;
    private GameObject _shopUI;
    void Start(){
        // Transform shopUI = Instantiate(GameAssets.i.shopUI, Vector3.zero, Quaternion.identity);
        _shopUI = Instantiate(shopUI);
        _shopUI.SetActive(false);
        Debug.Log(_shopUI);
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log(other.tag + "enters shop");
        if (other.CompareTag("Player")) _shopUI.SetActive(true);
        
    }

    void OnTriggerExit2D(Collider2D other){
        Debug.Log(other.tag + " exited shop");
        if (other.CompareTag("Player")) _shopUI.SetActive(false);
    }
}
