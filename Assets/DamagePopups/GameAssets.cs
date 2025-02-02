﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour {

    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null){
                _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return _i;
        }
    }

    public Transform pfDamagePopup;
    public Transform pfDeathCoin;
    public Transform gameOverUI;
    public Transform debugUI;

    public Transform shopUI;
}

