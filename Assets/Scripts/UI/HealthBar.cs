using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        fill.fillAmount = (float) player.GetComponent<PlayerStats>().currentHealth/ (float) player.GetComponent<PlayerStats>().maxHealth.GetValue();
    }
}
