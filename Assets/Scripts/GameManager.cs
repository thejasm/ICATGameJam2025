using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string previousScene; // Stores the last scene before entering Settings

    public static void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") { previousScene = "MainMenu"; }
        previousScene = SceneManager.GetActiveScene().name; // Save the current scene name
        Debug.Log("Saving Previous Scene: " + previousScene); // Debug message
        SceneManager.LoadScene(sceneName); // Load new scene
    }

    public static void GoBack()
    {
        if (!string.IsNullOrEmpty(previousScene)) // Check if previousScene is not empty
        {
            Debug.Log("Going back to: " + previousScene); // Debug message
            SceneManager.LoadScene(previousScene); // Load last saved scene
        }
        else
        {
            Debug.LogError("No previous scene stored!"); // Error message
        }
    }
    void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        if (SceneManager.GetActiveScene().name == "MainMenu") { previousScene = "MainMenu"; }
        DontDestroyOnLoad(gameObject); // Make it persist across scenes
    }

}
