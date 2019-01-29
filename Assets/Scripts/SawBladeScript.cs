using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeScript : MonoBehaviour {

    [SerializeField] private List<Sprite> _bloodySprites = new List<Sprite>();
    [SerializeField] private GameObject _blood;

    private SpriteRenderer _renderer;
    private int _spriteIndex = 0;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update () {
        RotateBlade();
	}

    void RotateBlade()
    {
        transform.Rotate(new Vector3(0, 0, 360.0f * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"
            || collision.tag == "PlayerCorpse")
        {

            Instantiate(_blood, transform.position, Quaternion.identity);

            _renderer.sprite = _bloodySprites[_spriteIndex];
            ++_spriteIndex;
            _spriteIndex = Mathf.Clamp(_spriteIndex, 0, _bloodySprites.Count - 1);
        }
    }
}
