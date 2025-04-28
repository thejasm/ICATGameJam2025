using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public void Back()
    {
        GameManager.GoBack(); // This should return to PlayGame Scene or Main Menu
    }
}
