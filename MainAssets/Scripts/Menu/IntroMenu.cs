using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    /// <summary>
    /// Handles the on click event from the play button
    /// </summary>
    public void HandleStartButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
    }
}
