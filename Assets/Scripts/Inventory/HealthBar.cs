using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image bar;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        bar = gameObject.GetComponentInChildren<Image>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void Update() {
        // bar.fillAmount = player.maxHealth/player.currentHealth;
    }
}
