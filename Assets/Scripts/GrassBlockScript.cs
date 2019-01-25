using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBlockScript : MonoBehaviour {

    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

    private void Start()
    {
        int index = Random.Range(0, _sprites.Count);

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        renderer.sprite = _sprites[index];
    }
}
