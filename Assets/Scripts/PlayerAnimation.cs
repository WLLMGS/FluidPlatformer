using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimation : MonoBehaviour
{

    private Animator _anim;
    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private PlayerMovement _playerMovement;

    private bool _DoDash = false;

    public bool DoDash
    {
        get { return _DoDash; }
        set { _DoDash = value; }
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _anim.speed = 2.0f;
    }

    private void Update()
    {
        CheckState();
        CheckDirection();
    }

    void CheckState()
    {

        if (!_playerMovement.DoJump)
        {
            _anim.SetBool("IsJumping", false);
        }

        if (_DoDash)
        {
            //_anim.speed = 0.1f;
            _anim.SetTrigger("IsDashing");
            _DoDash = false;
        }
        else if (_playerMovement.DoJump)
        {
            _anim.SetBool("IsJumping", true);
        }
        else if (_rb.velocity.x != 0.0f)
        {
            _anim.SetBool("IsRunning", true);
            _anim.speed = 2.0f;
        }
        else
        {
            _anim.SetBool("IsRunning", false);
            _anim.speed = 1.0f;
        }
    }

    void CheckDirection()
    {
        //if (_rb.velocity.x < 0.0f) _renderer.flipX = true;
        //else if (_rb.velocity.x > 0.0f) _renderer.flipX = false;
        if (_rb.velocity.x < 0.0f) transform.localScale = new Vector3(-1.0f,1.0f,1);
        else if (_rb.velocity.x > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1);
    }
}
