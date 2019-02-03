using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakBlockScript : MonoBehaviour
{
    [SerializeField] private GameObject _particleSystem;

    private Animator _animator;
    private bool _isActivated = false;
    private Vector2 _startPos;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _startPos = transform.position;
    }

    private void Activate()
    {
        _isActivated = true;
        //spawn particle system
        Instantiate(_particleSystem, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player enters -> start cooldown & animation
        //after x amount of seconds start falling (gravity scale = 1)
        //when block collides with ground destroy it
        if (collision.gameObject.tag == "Player")
        {
            _animator.SetTrigger("Activate");
            Invoke("Activate", 1.0f);
        }
    }

}
