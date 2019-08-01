using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Listens for the OnClick events for the main menu buttons
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Handles the on click event from the play button
    /// </summary>
    public void HandlePlayButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Game);                
    }

    /// <summary>
    /// Handles the on click event from the Controls button
    /// </summary>
    public void HandleControlsButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Controls);
    }

    /// <summary>
    /// Handles the on click event from the Info button
    /// </summary>
    public void HandleInfoButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Info);
    }

    /// <summary>
    /// Handles the on click event from the quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }
}
