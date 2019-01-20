using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelMenuManager : MonoBehaviour {

    [SerializeField] private GameObject _menu;

    private bool _IsPaused = false;

    private void Start()
    {
        _menu.SetActive(false);
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_IsPaused) UnPause();
            else Pause();
        }
    }

    private void  Pause()
    {
        _IsPaused = true;
        Time.timeScale = 0.0f;
        _menu.SetActive(true);
    }
    private void UnPause()
    {
        _IsPaused = false;
        Time.timeScale = 1.0f;
        _menu.SetActive(false);
    }

}
