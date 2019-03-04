using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("LevelSelectionScene");
    }

    public void MapEditorPressed()
    {
        SceneManager.LoadScene("Editor");
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
