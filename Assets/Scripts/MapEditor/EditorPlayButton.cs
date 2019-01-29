using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorPlayButton : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _editorCamera;
    [SerializeField] private GameObject _followCamera;

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
            Vector3 pos = new Vector3(beginning.transform.position.x, beginning.transform.position.y, 15);
            var p = Instantiate(_player, pos, Quaternion.identity);
            //disable the editor camera
            _editorCamera.SetActive(false);

            //spawn follow camera
            Instantiate(_followCamera, pos, Quaternion.identity);
        }
       
    }

    bool IsPlayerAlreadySpawned()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        return (player != null);
    }
}
