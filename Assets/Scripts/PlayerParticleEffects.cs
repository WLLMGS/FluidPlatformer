using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEffects : MonoBehaviour {

    [SerializeField] private GameObject _landEffect;

    private PlayerCollisions _collisions;
    private Rigidbody2D _rigid;

    private bool _previouslyGrounded = true;

    private void Start()
    {
        _collisions = GetComponent<PlayerCollisions>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleLanding();
    }

    private void HandleLanding()
    {
        if(!_previouslyGrounded
            && _collisions.Down
            && _rigid.velocity.y <= 0.0f)
        {
            Debug.Log("yes sir");
            Vector3 pos = transform.position + new Vector3(0.5f,-0.5f, 0);
            var inst =Instantiate(_landEffect, pos, Quaternion.identity);
        }

        _previouslyGrounded = _collisions.Down;
    }
}
