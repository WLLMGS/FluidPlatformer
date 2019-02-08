using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadScript : MonoBehaviour {

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerMovement scr = collision.gameObject.GetComponent<PlayerMovement>();
            if (!scr) return;
            scr.DoJump = true;
            scr.JumpMultiplier = 2.5f;
            _anim.SetTrigger("Bounce");
        }
    }
}
