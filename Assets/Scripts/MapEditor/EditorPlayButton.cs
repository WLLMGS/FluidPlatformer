using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorPlayButton : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _editorCamera;
    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(SpawnPlayer);
    }

    private void SpawnPlayer()
    {
        if(!IsPlayerAlreadySpawned())
        {
            Debug.Log("Player spawned");
            //check if there is a player start
            var beginning = GameObject.FindGameObjectWithTag("LevelBegin");
            //if there is not player start return null
            if (beginning == null) return;
            //instantiate the player at the player start location
            var p = Instantiate(_player, beginning.transform.position, Quaternion.identity);
            //disable the editor camera
            _editorCamera.SetActive(false);
        }
       
    }

    bool IsPlayerAlreadySpawned()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        return (player != null);
    }
}
