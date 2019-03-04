using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuButton : MainMenuButton{

    [SerializeField] private int _levelNumber = 1;

    public override void DoAction()
    {
        string sceneName = "Level" + _levelNumber.ToString();
        SceneManager.LoadScene(sceneName);
    }
}
