using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController1 : MonoBehaviour {

    [SerializeField] private List<MainMenuButton> _buttons = new List<MainMenuButton>();
    private int _index = 0;
    private bool _canNavigate = true;
    private float _navCooldown = 0.20f;
    private ControllerNavigationComponent _navigationController;
    private void Start()
    {
        _buttons[_index].Activate();
        _navigationController = GetComponent<ControllerNavigationComponent>();
    }

    private void Update()
    {
        if(_navigationController.IsReady) HandleInput();
    }

    private void HandleInput()
    {
        float axis = Input.GetAxis("Vertical");

        if (_canNavigate
            && axis > 0.0f
            && _index > 0)
        {
            _index -= 1;
            UpdateButton(true);
        }
        else if (_canNavigate
            && axis < 0.0f
            && _index < _buttons.Count - 1)
        {
            _index += 1;
            UpdateButton(false);
        }

        if (Mathf.Approximately(axis, 0.0f)) _canNavigate = true;

    }

    private void UpdateButton(bool up)
    {
        _canNavigate = false;
        _buttons[_index].Activate();
        int prevIndex = (up) ? _index + 1 : _index - 1;
        _buttons[prevIndex].Deactivate();
    }
}
