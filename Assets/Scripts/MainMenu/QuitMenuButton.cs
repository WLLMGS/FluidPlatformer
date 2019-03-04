using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenuButton : MainMenuButton {

    public override void DoAction()
    {
        Application.Quit();
    }
}
