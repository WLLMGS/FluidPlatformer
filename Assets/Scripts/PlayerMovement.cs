using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movespeed = 5.0f;
    [SerializeField] private float _maxMoveSpeed = 15.0f;

    [SerializeField] private float _jumpForce = 5.0f;
    private Rigidbody2D _rigid;
    private PlayerCollisions _collisions;

    private bool _isGrounded = false;

    private bool _doJump = false;
    private float _horzAxis = 0.0f;
    private float _maxSlideSpeed = 5.0f;
    private float _maxUpSpeed = 25.0f;

    private void Start()
    {
        _collisions = GetComponent<PlayerCollisions>();
        _rigid = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        CheckGrounded();

        _horzAxis = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _doJump = true;
        }

    }

    private void FixedUpdate()
    {
        ClampMovespeed();
        HandleMovement();
        HandleWallSlide();
        HandleJumping();
    }

    void ClampMovespeed()
    {
        //clamp the speed
        if (Mathf.Abs(_rigid.velocity.x) > _maxMoveSpeed)
        {
            _rigid.velocity = new Vector2(_maxMoveSpeed * Mathf.Sign(_rigid.velocity.x), _rigid.velocity.y);
        }

        //set velocity to zero if the player is on the ground and is not moving the stick
        if ((_collisions.Down && Mathf.Approximately(_horzAxis, 0.0f))) _rigid.velocity = new Vector2(0, _rigid.velocity.y);

        //clamp the up speed
        if (_rigid.velocity.y > _maxUpSpeed) _rigid.velocity = new Vector2(_rigid.velocity.x, _maxUpSpeed);
    }


    void HandleMovement()
    {
        //_rigid.velocity += new Vector2(_horzAxis * _maxMoveSpeed, 0.0f);
        _rigid.AddForce(new Vector2(_horzAxis * _movespeed, 0.0f), ForceMode2D.Force);
    }

    void HandleJumping()
    {
        if (_doJump)
        {
            _doJump = false;

            if (_isGrounded)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, 0.0f);
                _rigid.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            }
            else if (_collisions.Right && !_collisions.Down)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, 0.0f);
                _rigid.AddForce(new Vector2(-_jumpForce * 4, _jumpForce), ForceMode2D.Impulse);
            }
            else if (_collisions.Left && !_collisions.Down)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, 0.0f);
                _rigid.AddForce(new Vector2(_jumpForce * 4, _jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void CheckGrounded()
    {
        if (_collisions.Down) _isGrounded = true;
        else _isGrounded = false;
    }

    void HandleWallSlide()
    {
        if ((_collisions.Left || _collisions.Right) && !_collisions.Down)
        {
            if (_rigid.velocity.y < -_maxSlideSpeed)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, -_maxSlideSpeed);
            }
        }
    }
}
