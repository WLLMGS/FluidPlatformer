using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNavigationComponent : MonoBehaviour {

    [SerializeField] private ControllerNavigationComponent _previousController;
    [SerializeField] private ControllerNavigationComponent _nextController;
    [SerializeField] private bool _isActive = false;

    private Camera _cam;
    private float _orgCamZ;
    private float _camMovespeed = 25.0f;
    private bool _isReady = false;

    public ControllerNavigationComponent PreviousController
    {
        get { return _previousController; }
    }
    public ControllerNavigationComponent NextController
    {
        get { return _nextController; }
    }
    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    public bool IsReady
    {
        get { return _isReady; }
    }

    private void Start()
    {
        _cam = Camera.main;
        _orgCamZ = _cam.transform.position.z;
    }

    private void Update()
    {
        HandleControls();
        HandleCamera();
    }

    private void HandleControls()
    {
        if(_previousController
            && Input.GetKeyDown(KeyCode.Escape))
        {
            //activate previous controller
            _previousController.IsActive = true;
            this.IsActive = false;
        }
    }

    private void HandleCamera()
    {
        if(_isActive)
        {
            var currPos = _cam.transform.position;
            var targetPos = transform.position;
            targetPos.z = _orgCamZ;

            _cam.transform.position = Vector3.MoveTowards(currPos, targetPos, _camMovespeed * Time.deltaTime);

            float d = Vector2.Distance(currPos, targetPos);

            if (d <= 1.0f) _isReady = true;
            else _isReady = false;
        }
    }

    public void GoToNextController()
    {
        if(_nextController)
        {
            _nextController.IsActive = true;
            this.IsActive = false;
        }
    }
}
