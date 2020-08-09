using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int attackPrice;

    public int healthPrice;

    public GameObject goldWarning;

    private bool checkGold(int playerGold, int price){
        if (playerGold < price) {
            return false;
        } else {
            return true;
        }
    }

    public void buyAttack(){
        if (checkGold(Player.instance.gold, attackPrice)){
            Player.instance.gold -= attackPrice;
            Player.instance.characterStats.damage.baseValue++;
            attackPrice++;
        }
    }

    public void buyHealth(){
        if (checkGold(Player.instance.gold, healthPrice)){
            Player.instance.gold -= healthPrice;
            Player.instance.characterStats.maxHealth.baseValue += 10;
            healthPrice++;
        }
    }
}
