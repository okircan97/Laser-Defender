using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is to make the scenes load.
public class Level : MonoBehaviour
{
    // This method is to load game over scene.
    public void LoadGameOver()
    {
        StartCoroutine("WaitAndLoad");
    }

    // This method is to load game scene.
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    // This method is to load start menu. It'll also reset the score over a GameSession object.
    public void LoadStartMenu()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.ResetGame();
        SceneManager.LoadScene("Start Menu");
    }

    // This method is to load the credits.
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    // This method is to quit game.
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad() 
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game Over");
    }
}
