using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButtonScript : MonoBehaviour
{
    [SerializeField] Sprite _normal;
    [SerializeField] Sprite _hovering;
    [SerializeField] private int _levelId = 1;


    private SpriteRenderer _renderer = null;
    private bool _isHovering = false;
    private string _levelName = "";

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _normal;
        _levelName = "level" + _levelId.ToString();
        
    }

    private void Update()
    {
        if(_isHovering
            && Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(_levelName);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _renderer.sprite = _hovering;
            _isHovering = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _renderer.sprite = _normal;
            _isHovering = false;
        }
    }
}
