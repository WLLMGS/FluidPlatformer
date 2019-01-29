using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorStopButton : MonoBehaviour
{
    [SerializeField] private GameObject _editorCamera;

    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(StopPlayTesting);
    }

    private void StopPlayTesting()
    {
        //find player
        var player = GameObject.FindGameObjectWithTag("Player");
        //if player is not found return 
        if (player == null) return;
        //destroy the player
        Destroy(player);

        //destroy follow camera
        var cam = GameObject.FindGameObjectWithTag("FollowCamera");
        if (cam) Destroy(cam);

        //reenable the editor camera
        _editorCamera.SetActive(true);
    }
}
