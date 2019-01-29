using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

    private static CameraShaker _instance = null;

    public static CameraShaker Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Shake()
    {
        _anim.SetTrigger("Shake");
    }

}
