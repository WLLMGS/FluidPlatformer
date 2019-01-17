using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerCollisions _collisions;
    private Rigidbody2D _rb;
    private float _movespeed = 10.0f;
    private float _jumpforce = 15.0f;

    private float _horzAxis = 0;
    private float _vertAxis = 0;
    private bool _doJump = false;
    private bool _canJump = false;
    private float _gravityScale = 0;

    private float _dashTimer = 0.0f;
    private float _dashCooldown = 0.1f;

    private float _jumpCooldown = 0.1f;
    private bool _IsGrounded = false;

    private float _dashSpeed = 50.0f;
    private float _dashDirX = 1;
    private float _dashDirY = 1;
    private bool _IsDashing = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gravityScale = _rb.gravityScale;
        _collisions = GetComponent<PlayerCollisions>();

    }

    private void Update()
    {
        _horzAxis = Input.GetAxis("Horizontal");
        _vertAxis = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)
        && _canJump)
        {
            _doJump = true;
            StartCoroutine(JumpCooldown());
        }

        if (Input.GetMouseButtonDown(1))
        {
            _dashDirX = _horzAxis > 0 ? 1.0f : -1.0f ;
            _dashDirY = 0.0f;


            _IsDashing = true;
            StartCoroutine(DashCooldown());
        }

    }

    private void FixedUpdate()
    {
        CheckCollisions();
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if (!_IsDashing)
        {
            _rb.velocity = new Vector2(_horzAxis * _movespeed, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(_dashDirX * _dashSpeed, _rb.velocity.y);
        }
    }

    private void HandleJump()
    {
        if (_doJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce);
        }
    }

    private void CheckCollisions()
    {
        if (_collisions.Down) _canJump = true;
        else _canJump = false;

        if ((_collisions.Right || _collisions.Left))
        {
            //climb
        }
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _doJump = false;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(_dashCooldown);
        _IsDashing = false;
    }
}
