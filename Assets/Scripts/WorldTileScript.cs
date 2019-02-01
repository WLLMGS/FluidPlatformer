using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileScript : MonoBehaviour {

    [SerializeField] private Sprite _TopCorner;
    [SerializeField] private Sprite _TopMiddle;
    [SerializeField] private Sprite _SideMiddle;
    [SerializeField] private Sprite _BottomCorner;
    [SerializeField] private Sprite _BottomMiddle;
    [SerializeField] private Sprite _MiddleMiddle;

    private SpriteRenderer _renderer;
    private Collider2D _collider;
    private BlockType _ID;

    public int ID
    {
        set { _ID = (BlockType)value; }
    }

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        if (_renderer && _collider) InitBlock();
    }

    private void InitBlock()
    {
        switch(_ID)
        {
            case BlockType.TOP_LEFT:
                _renderer.flipX = false;
                _renderer.sprite = _TopCorner;
                break;
            case BlockType.TOP_MIDDLE:
                _renderer.flipX = false;
                _renderer.sprite = _TopMiddle;
                break;
            case BlockType.TOP_RIGHT:
                _renderer.flipX = true;
                _renderer.sprite = _TopCorner;
                break;
            case BlockType.SIDE_MIDDLE_LEFT:
                _renderer.flipX = false;
                _renderer.sprite = _SideMiddle;
                break;
            case BlockType.SIDE_MIDDLE_RIGHT:
                _renderer.flipX = true;
                _renderer.sprite = _SideMiddle;
                break;
            case BlockType.MIDDLE_MIDDLE:
                Destroy(_collider);
                _renderer.flipX = false;
                _renderer.sprite = _MiddleMiddle;
                break;
            case BlockType.BOTTOM_LEFT:
                _renderer.flipX = false;
                _renderer.sprite = _BottomCorner;
                break;
            case BlockType.BOTTOM_MIDDLE:
                _renderer.flipX = false;
                _renderer.sprite = _BottomMiddle;
                break;
            case BlockType.BOTTOM_RIGHT:
                _renderer.flipX = true;
                _renderer.sprite = _BottomCorner;
                break;
        }
    }
}
