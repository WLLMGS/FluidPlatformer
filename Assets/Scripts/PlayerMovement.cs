using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private static bool _CanMove = false;

    public static bool CanMove
    {
        get { return _CanMove; }
        set { _CanMove = value; }
    }

    private PlayerCollisions _collisions;
    private Rigidbody2D _rb;
    private PlayerAnimation _anim;

    private float _movespeed = 10.0f;
    private float _jumpforce = 15.0f;
    private float _wallJumpForce = 20.0f;

    private float _horzAxis = 0;
    private bool _doJump = false;
    private bool _canJump = false;
    private float _dashCooldown = 0.1f;
    private bool _TouchingWall = false;
    private float _jumpCooldown = 0.1f;
    private float _wallJumpCooldown = 0.1f;

    private float _dashSpeed = 50.0f;
    private float _dashDirX = 1;
    private bool _IsDashing = false;


    private bool _doWallJump = false;
    private int _wallJumpDir = 1;

    private float _jumpMultiplier = 1.0f;

    public bool DoJump
    {
        set
        {
            _doJump = value;
            StartCoroutine(JumpCooldown());
        }
        get { return _doJump; }
    }

    public float JumpMultiplier
    {
        get { return _jumpMultiplier; }
        set { _jumpMultiplier = value; }
    }

    public bool IsDashing
    {
        get { return _IsDashing; }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collisions = GetComponent<PlayerCollisions>();
        _anim = GetComponent<PlayerAnimation>();

        //set player to follow
        PlayerFollowCameraScript.Player = gameObject;
    }

    private void Update()
    {
        if (_CanMove)
        {
            _horzAxis = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump")
            && _canJump
            && _collisions.Down)
            {
                _doJump = true;
                StartCoroutine(JumpCooldown());
            }
            else if (Input.GetButtonDown("Jump")
                && _TouchingWall
                && !_doWallJump
                && !_collisions.Down)
            {
                if (_collisions.Right) _wallJumpDir = -1;
                else _wallJumpDir = 1;

                _doWallJump = true;
                StartCoroutine(WallJumpCooldown());
            }

            if (Input.GetMouseButtonDown(1)
                || Input.GetButtonDown("Roll"))
            {
                _dashDirX = _horzAxis > 0 ? 1.0f : -1.0f;

                _anim.DoDash = true;

                _IsDashing = true;
                StartCoroutine(DashCooldown());
            }
        }

    }

    private void FixedUpdate()
    {
        if (_CanMove)
        {
            CheckCollisions();
            HandleMovement();
            HandleJump();
        }

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
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce * _jumpMultiplier);
        }

        if (_doWallJump)
        {
            _rb.velocity = new Vector2(_wallJumpForce * _wallJumpDir, _wallJumpForce);
        }
    }

    private void CheckCollisions()
    {
        if (_collisions.Down) _canJump = true;
        else _canJump = false;

        if ((_collisions.Right || _collisions.Left))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -2.0f);
            _TouchingWall = true;
        }
        else _TouchingWall = false;

    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _doJump = false;
        _jumpMultiplier = 1.0f;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(_dashCooldown);
        _IsDashing = false;
    }

    private IEnumerator WallJumpCooldown()
    {
        yield return new WaitForSeconds(_wallJumpCooldown);
        _doWallJump = false;
    }
}
