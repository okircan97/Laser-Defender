using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is to make changes over the score.
public class GameSession : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////

    int score = 0;  


    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////

    private void Awake(){
        SetUpSingleton();
    }


    //////////////////////////////////
    //////////// METHODS /////////////
    //////////////////////////////////

    // This method is to set up the singleton design.
    private void SetUpSingleton(){
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    // Getter method for the score.
    public int GetScore(){
        return score;
    }

    // This method is to increase the score.
    public void AddToScore(int numberOfKills){
        score += numberOfKills;
    }

    // This method is to reset the score by destroying the game session object.
    public void ResetGame(){
        Destroy(gameObject);
    }
}
