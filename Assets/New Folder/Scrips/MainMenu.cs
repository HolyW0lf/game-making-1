using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene when the "Start" button is clicked.
        SceneManager.LoadScene("GameScene"); // Replace "GameScene" with your actual game scene name.
    }

    public void ExitGame()
    {
        // Quit the application when the "Exit" button is clicked.
        Application.Quit();
    }
}