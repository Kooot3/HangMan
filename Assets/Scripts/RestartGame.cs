 using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;

 namespace Hangman.Scripts
{
    public class RestartGame : MonoBehaviour

    public GameObject gameOverUILose;
    public GameObject gameOverUIWin;
    
    public void gameOverLose()
    {
        object gameOverUILose;
        gameOverUILose.SetActive(true);
    }

    public void gameOverWin()
    {
        gameOverUIWin.SetActive(true);
    }
    
    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}