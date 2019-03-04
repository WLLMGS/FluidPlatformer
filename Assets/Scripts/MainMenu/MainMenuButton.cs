using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour {

    private bool _isActive = false;
    protected GameObject _parent;
    private ControllerNavigationComponent _controller;

    private void Start()
    {
        _parent = transform.parent.gameObject;
        _controller = _parent.GetComponent<ControllerNavigationComponent>();
    }

    private void Update()
    {
        if(_controller.IsReady) HandleControls();
    }

    private void HandleControls()
    {
        if(_isActive
            && Input.GetKeyDown(KeyCode.Return))
        {
            DoAction();
        }
    }

    public void Activate()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        _isActive = true;
    }

    public void Deactivate()
    {
        transform.localScale = new Vector3(1, 1, 1);
        _isActive = false;
    }

    virtual public void DoAction()
    {

    }


}


