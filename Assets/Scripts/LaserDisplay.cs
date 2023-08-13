using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Bu class player'ın laserPrefab'inin DamageDealer'ının içindeki damage float'unu ekrana yansıtmak için.

public class LaserDisplay : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////    
    TMP_Text laserText;
    Player player;


    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////
    void Start()
    {
        laserText = GetComponent<TMP_Text>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (FindObjectOfType<Player>())
        {
            laserText.text = player.GetLaserDamage().ToString();
        }
    }
}
