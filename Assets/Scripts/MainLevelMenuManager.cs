using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainLevelMenuManager : MonoBehaviour {

    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _firstButton;
    [SerializeField] private EventSystem _eventSystem;
    private bool _IsPaused = false;

    private void Start()
    {
        _menu.SetActive(false);
    }

    private void Update()
    {
       if(Input.GetButtonDown("Start"))
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
        StartCoroutine(SelectFirstButton());

    }
    private void UnPause()
    {
        _IsPaused = false;
        Time.timeScale = 1.0f;
        _menu.SetActive(false);
    }

    //BUTTON FUNCTIONS
    public void ResumeButton()
    {
        UnPause();
    }

    public void MapEditorButton()
    {
        UnPause();
        SceneManager.LoadScene("Editor");
    }

    public void LevelSelectButton()
    {
        UnPause();
        SceneManager.LoadScene("LevelSelectionScene");
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        UnPause();
        SceneManager.LoadScene("NewMainMenu");
    }

    private IEnumerator SelectFirstButton()
    {
        _eventSystem.SetSelectedGameObject(null);
        yield return null;
        _eventSystem.SetSelectedGameObject(_eventSystem.firstSelectedGameObject);
    }
}
