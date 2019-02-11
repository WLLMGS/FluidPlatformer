using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenuItem : MonoBehaviour
{

    [SerializeField] private int _sceneIndex = 1;

    public int SceneIndex
    {
        get { return _sceneIndex; }
    }

    public void SetSelected()
    {
       transform.localScale = new Vector3(1.25f, 1.25f, 1);
    }

    public void SetDeselected()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
