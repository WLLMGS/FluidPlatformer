using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    [SerializeField] private MapLoader _mapLoader;
    [SerializeField] private GameObject _player;

    private GameObject _levelParent;
    private GameObject _levelStart;

    private string _rootPath = "Levels/";
    private string _extention = ".bin";
    private void Start()
    {
        //make parent object
        _levelParent = new GameObject("Level");
        //load initial level
        _mapLoader.LoadLevel(_rootPath + "levelVideo" + _extention, _levelParent.transform);
        //get level start
        _levelStart = GameObject.FindGameObjectWithTag("LevelBegin");
        //spawn player
        Vector3 startPos = new Vector3(_levelStart.transform.position.x, _levelStart.transform.position.y, 15);
        Instantiate(_player, startPos, Quaternion.identity);
    }

}
