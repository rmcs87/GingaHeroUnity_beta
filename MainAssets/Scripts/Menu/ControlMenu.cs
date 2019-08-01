using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    public void HandleReturnButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
    }
}
