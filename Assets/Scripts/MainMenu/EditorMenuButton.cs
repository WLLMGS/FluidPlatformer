using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorMenuButton : MainMenuButton {

	public override void DoAction()
    {
        SceneManager.LoadScene("Editor");
    }
}
