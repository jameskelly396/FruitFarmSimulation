using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /* MainMenu.cs
     * By: James Kelly
     * Purpose: Play and quit button functions for main menu scene
     */
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
