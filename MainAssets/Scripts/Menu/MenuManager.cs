using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages navigation through the menu system
/// </summary>
public static class MenuManager
{
    /// <summary>
    /// Goes to the menu with the given name
    /// </summary>
    /// <param name="name">name of the menu to go to</param>
    public static void GoToMenu(MenuName name)
    {
        switch (name)
        {
            case MenuName.Intro:
                // go to Intro scene
                SceneManager.LoadScene("0-Intro");                
                break;
            case MenuName.Game:
                // go to Intro scene
                EventManager.TriggerEvent(EventName.GameStartedEvent);
                SceneManager.LoadScene("2-Game");                
                break;
            case MenuName.Controls:
                // go to Intro scene
                SceneManager.LoadScene("1.1-Controls");
                break;
            case MenuName.Info:
                // go to Intro scene
                SceneManager.LoadScene("1.2-About");
                break;
            case MenuName.Main:
                // go to MainMenu scene
                EventManager.TriggerEvent(EventName.GameOverEvent);
                SceneManager.LoadScene("1-MainMenu");
                break;
            case MenuName.Pause:
                // instantiate prefab
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;
        }
    }
}
