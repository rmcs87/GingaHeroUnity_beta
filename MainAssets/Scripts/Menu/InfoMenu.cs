using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    public void HandleReturnButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
    }
}
