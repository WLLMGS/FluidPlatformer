using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

    //===== SINGLETON =====
    private static GameplayManager _instance = null;

    public static GameplayManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    //===== MAIN =====
    [SerializeField] private MapLoader _mapLoader;
    [SerializeField] private GameObject _player;
    [SerializeField] private string _filename = "levelVideo";

    private GameObject _levelParent;
    private GameObject _levelStart;
    private Vector3 _startPos;

    private string _rootPath = "Levels/";
    private string _extention = ".bin";

    private bool _isPlayerDead = false;

    private List<ResetObjectScript> _resetableObjects;

    private void Start()
    {
        //make parent object
        _levelParent = new GameObject("Level");
        //load initial level
        _resetableObjects = _mapLoader.LoadLevel(_rootPath + _filename + _extention, _levelParent.transform);
        //get level start
        _levelStart = GameObject.FindGameObjectWithTag("LevelBegin");
        //spawn player
        _startPos = new Vector3(_levelStart.transform.position.x, _levelStart.transform.position.y, 15);
        SpawnPlayer();
    }

    private void Update()
    {
        HandleRespawning();
    }

    public void NotifyPlayerDeath()
    {
        _isPlayerDead = true;
    }

    public void NotifyLevelFinish()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    private void HandleRespawning()
    {
        if (_isPlayerDead
            && (Input.GetKeyDown(KeyCode.R)
            || Input.GetButtonDown("Jump")))
        {
            PlayerMovement.CanMove = false;
            _isPlayerDead = false;
            //respawn player
            SpawnPlayer();

            foreach (var comp in _resetableObjects) comp.Reset();

        }
    }

    private void SpawnPlayer()
    {
        Instantiate(_player, _startPos, Quaternion.identity);
    }

}
