using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play button clicked");
        SceneManager.LoadScene(1); // Change "GameScene" to your actual game scene name
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(2); // Change to your settings scene
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!"); // Won't work in the editor, but works in a built game.
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene(3); 
    }

}
