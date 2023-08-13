using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class' purpose is to make the music play continiously.
public class MusicPlayer : MonoBehaviour
{
    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////

    void Awake()
    {
        SetUpSingleton();
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Game Over")
        {
            Destroy(gameObject);
        }
    }

    //////////////////////////////////
    //////////// METHODS /////////////
    //////////////////////////////////

    // This method is to set up the signleton design.
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }
}
