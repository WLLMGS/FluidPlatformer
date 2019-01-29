using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlayerScript : MonoBehaviour {

    private Rigidbody2D _rigid;
    private float _range = 25.0f;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        float x = Random.Range(-_range, _range);
        float y = Random.Range(-_range, _range);
        Vector2 force = new Vector2(x, y);
        _rigid.AddForce(force, ForceMode2D.Impulse);
    }
}
