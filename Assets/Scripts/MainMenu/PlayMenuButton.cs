using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuButton : MainMenuButton
{

    [SerializeField] private ControllerNavigationComponent _levelSelectController;

    public override void DoAction()
    {
        var controller = _parent.GetComponent<ControllerNavigationComponent>();

        if (controller) controller.IsActive = false;

        _levelSelectController.IsActive = true;

    }
}
