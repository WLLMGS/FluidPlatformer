using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraMovement : MonoBehaviour
{
    private float _movespeed = 10.0f;
    private float _scrollSpeed = 5.0f;
    private Camera _camera = null;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleMovement();
        HandleCameraZoom();
    }

    private void HandleMovement()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        transform.position += new Vector3(horz, vert, 0) * _movespeed * Time.deltaTime;
    }
    private void HandleCameraZoom()
    {
        float mouseScroll = -Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
        float newSize = _camera.orthographicSize + mouseScroll;

        newSize = Mathf.Clamp(newSize, 1.0f, 15.0f);

        _camera.orthographicSize = newSize;
    }

}
