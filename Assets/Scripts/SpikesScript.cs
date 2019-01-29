using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
    [SerializeField] private GameObject _blood;

    private SpriteRenderer _renderer;
    private int _spriteIndex = 0;

    private void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"
            || collision.tag == "PlayerCorpse")
        {
            Instantiate(_blood, transform.position, Quaternion.identity);

            _renderer.sprite = _sprites[_spriteIndex];
            _spriteIndex++;
            _spriteIndex = Mathf.Clamp(_spriteIndex, 0, _sprites.Count - 1);
        }
    }

    
}
