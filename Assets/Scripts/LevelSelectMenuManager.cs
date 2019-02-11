using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenuManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelsButtons = new List<GameObject>();
    [SerializeField] private LevelSelectMenuCameraScript _camera;

    private int _currentIndex = 0;
    private bool _canNavigate = true;
    private float _navigationCooldown = 0.2f;

    private void Start()
    {
        if (_levelsButtons.Count > 0)
        {
            //determine new camera pos
            Vector3 newPos = _levelsButtons[_currentIndex].transform.position;
            newPos.z = _camera.transform.position.z;
            //set camera to that pos
            _camera.transform.position = newPos;
            //set new pos as target
            _camera.Target = newPos;

            //set current button as selected
            var scr = _levelsButtons[_currentIndex].GetComponent<LevelSelectMenuItem>();
            scr.SetSelected();
        }
    }

    private void Update()
    {
        //check navigation controls
        CheckNavigationControls();
        //check selection controls
        CheckSelectionControls();
    }


    private void CheckNavigationControls()
    {
        float yaxis = Input.GetAxis("Vertical");

        if (_canNavigate && yaxis > 0)
        {
            // navigate up if current index is greater than 0
            if (_currentIndex > 0)
            {
                NavigateUp();
            }
        }
        else if (_canNavigate && yaxis < 0)
        {
            // navigate down if current index is smaller than count - 1
            if (_currentIndex < _levelsButtons.Count - 1)
            {
                NavigateDown();
            }
        }
        else if (Mathf.Approximately(yaxis, 0.0f))
        {
            // set _canNavigate to true if there are no buttons pressed
            _canNavigate = true;
        }

    }

    private void CheckSelectionControls()
    {
        if (Input.GetButtonDown("Jump"))
        {
            int index = _levelsButtons[_currentIndex].GetComponent<LevelSelectMenuItem>().SceneIndex;
            SceneManager.LoadScene(index);
        }
    }

    private void NavigateUp()
    {
        //deselect current selected button
        var scr = _levelsButtons[_currentIndex].GetComponent<LevelSelectMenuItem>();
        scr.SetDeselected();

        //reduce current index
        _currentIndex -= 1;
        //transition camera
        TransitionCamera();

        //select current selected button
        scr = _levelsButtons[_currentIndex].GetComponent<LevelSelectMenuItem>();
        scr.SetSelected();

        //set can navigate to false
        _canNavigate = false;
        //reset can navigate after cooldown seconds
        Invoke("ResetCanNavigate", _navigationCooldown);

    }

    private void NavigateDown()
    {
        //deselect current selected button
        var scr = _levelsButtons[_currentIndex].GetComponent<LevelSelectMenuItem>();
        scr.SetDeselected();
        
        //increment current index
        _currentIndex += 1;
        //transition camera
        TransitionCamera();

        //select current selected button
        scr = _levelsButtons[_currentIndex].GetComponent<LevelSelectMenuItem>();
        scr.SetSelected();

        //set can navigate to false
        _canNavigate = false;
        //reset can navigate after cooldown seconds
        Invoke("ResetCanNavigate", _navigationCooldown);
    }


    private void TransitionCamera()
    {
        //determine new position
        Vector3 newPos = _levelsButtons[_currentIndex].transform.position;
        newPos.z = _camera.transform.position.z;
        //set new position
        _camera.Target = newPos;
    }

    private void ResetCanNavigate()
    {
        //set can navigate back to true
        _canNavigate = true;
    }
}
