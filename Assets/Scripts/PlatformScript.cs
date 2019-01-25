using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    private PlatformEffector2D _effector;
    private bool _isColliding = false;
    private float _originalRotOffset = 0;

    private void Start()
    {
        _effector = GetComponent<PlatformEffector2D>();
        _originalRotOffset = _effector.rotationalOffset;
    }

    private void Update()
    {
        HandleDroppingDown();
    }

    private void HandleDroppingDown()
    {
        if(_isColliding
            && Input.GetKeyDown(KeyCode.S))
        {
            _effector.rotationalOffset = 180.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _isColliding = false;
            _effector.rotationalOffset = _originalRotOffset;
        }
    }

}
