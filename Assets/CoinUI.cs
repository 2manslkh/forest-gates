using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUI : MonoBehaviour
{
    private int gold;
    // public TextMesh goldText;
    public TextMeshProUGUI goldText;
    // Start is called before the first frame update
    private GameObject player;


    void Start()
    {
        goldText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");
        gold = Player.instance.gold;
        goldText.text = gold.ToString();
    }
}
