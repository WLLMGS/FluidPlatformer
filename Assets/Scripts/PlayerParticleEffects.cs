using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEffects : MonoBehaviour {

    [SerializeField] private GameObject _landEffect;

    private PlayerCollisions _collisions;

    private bool _previouslyGrounded = true;
    private float _airbornTimer = 0.0f;

    private void Start()
    {
        _collisions = GetComponent<PlayerCollisions>();
    }

    private void Update()
    {
        HandleLanding();
    }

    private void HandleLanding()
    {
        if(!_previouslyGrounded
            && _collisions.Down
            )
        {
            Vector3 pos = transform.position + new Vector3(0.5f,-0.5f, 0);
            Instantiate(_landEffect, pos, Quaternion.identity);

            //do camera shake
            if(_airbornTimer >= 0.75f)
            {
                CameraShaker.Instance.Shake();
            }
            //reset airborn timer -> not in air anymore
            _airbornTimer = 0.0f;
        }

        //restart the airborn timer if first jumps (dont know if necessary)
        if (_previouslyGrounded
            && !_collisions.Down)
        {
            //start timer
            _airbornTimer = 0.0f;
        }

        //reset the aiborn timer if colliding w/ walls -> cus wallslide
        if (_collisions.Right || _collisions.Left) _airbornTimer = 0;

        _airbornTimer += Time.deltaTime;

        _previouslyGrounded = _collisions.Down;
    }
}
