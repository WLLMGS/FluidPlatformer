using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {

    [SerializeField] private float _ttl = 2.0f;

    private void Start()
    {
        Invoke("Kill", _ttl);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
