using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Bu class player health'ını ekrana yansıtmak için.
public class HealthDisplay : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////
    TMP_Text healthText;
    Player player;

    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////
    void Start()
    {
        healthText = GetComponent<TMP_Text>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (FindObjectOfType<Player>())
        {
            healthText.text = FindObjectOfType<Player>().GetHealth().ToString();
        }
    }
}
