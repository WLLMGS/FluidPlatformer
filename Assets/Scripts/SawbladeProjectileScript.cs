using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeProjectileScript : MonoBehaviour {

    [SerializeField] private GameObject _blood;

    private Vector2 _direction = Vector2.zero;
    public Vector2 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    private Vector2 _velocity = new Vector2(20.0f, 20.0f);

    private Rigidbody2D _rigid;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        _rigid.velocity = new Vector2(_velocity.x * _direction.x, _velocity.y * _direction.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Block")
        {
            Destroy(gameObject);
        }
        else if(collision.tag == "Player"
            || collision.tag == "PlayerCorpse")
        {
            Instantiate(_blood, transform.position, Quaternion.identity);
        }
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, 360.0f * Time.deltaTime));
    }
}
