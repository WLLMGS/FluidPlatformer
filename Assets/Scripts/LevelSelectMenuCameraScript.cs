using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenuCameraScript : MonoBehaviour
{

    private Vector3 _target = Vector3.zero;
    private float _cameraSpeed = 10;

    public Vector3 Target
    {
        get { return _target; }
        set { _target = value; }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _cameraSpeed * Time.deltaTime);
    }
}
