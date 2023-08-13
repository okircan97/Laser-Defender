using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

// Bu class score bilgilerini ekrana yansıtmak için.
public class ScoreDisplay : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////
    TMP_Text scoreText;
    GameSession gameSession;

    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        scoreText.text = FindObjectOfType<GameSession>().GetScore().ToString();
    }
}
