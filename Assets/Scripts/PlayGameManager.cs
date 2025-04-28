using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        GameManager.LoadScene("MainMenu"); // Save scene before switching
    }

    public void OpenSettings()
    {
        GameManager.LoadScene("Options"); // Save scene before switching
    }
}
