using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    TOP_LEFT,
    TOP_MIDDLE,
    TOP_RIGHT,
    SIDE_MIDDLE_LEFT,
    SIDE_MIDDLE_RIGHT,
    MIDDLE_MIDDLE,
    BOTTOM_LEFT,
    BOTTOM_MIDDLE,
    BOTTOM_RIGHT
}

public class AutoTilingScript : MonoBehaviour
{
    [SerializeField] private Sprite _TopCorner;
    [SerializeField] private Sprite _TopMiddle;
    [SerializeField] private Sprite _SideMiddle;
    [SerializeField] private Sprite _BottomCorner;
    [SerializeField] private Sprite _BottomMiddle;
    [SerializeField] private Sprite _MiddleMiddle;

    private BlockType _ID = 0;

    public BlockType ID
    {
        get { return _ID; }
    }

    private SpriteRenderer _renderer;
    private LayerMask _mask;
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(0, 1);

        _renderer = GetComponent<SpriteRenderer>();
        _mask = 1 << 9;

        if (_renderer) CheckSurroundings();
        UpdateSurroundings();
    }


    public void CheckSurroundings()
    {
        _renderer = GetComponent<SpriteRenderer>();

        RaycastHit2D RaycastHitUp = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.zero, _mask);
        RaycastHit2D RaycastHitRight = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), Vector2.zero, _mask);
        RaycastHit2D RaycastHitDown = Physics2D.Raycast(transform.position + new Vector3(0, -1, 0), Vector2.zero, _mask);
        RaycastHit2D RaycastHitLeft = Physics2D.Raycast(transform.position + new Vector3(-1, 0, 0), Vector2.zero, _mask);

        bool hitUp = CheckRaycastHit(RaycastHitUp);
        bool hitRight = CheckRaycastHit(RaycastHitRight);
        bool hitDown = CheckRaycastHit(RaycastHitDown);
        bool hitLeft = CheckRaycastHit(RaycastHitLeft);



        //top left
        if (!hitUp
            && hitDown
            && !hitLeft
            && hitRight

            )
        {
            _renderer.flipX = false;
            _renderer.sprite = _TopCorner;
            _ID = BlockType.TOP_LEFT;
        }
        //top middle
        else if (!hitUp
            && hitDown
            && hitLeft
            && hitRight
           )
        {
            _renderer.flipX = false;
            _renderer.sprite = _TopMiddle;
            _ID = BlockType.TOP_MIDDLE;
        }
        //top right
        else if (!hitRight
            && !hitUp
            && hitDown
            && hitLeft)
        {
            _renderer.flipX = true;
            _renderer.sprite = _TopCorner;
            _ID = BlockType.TOP_RIGHT;
        }
        //side middle left
        else if (!hitLeft
                && hitDown
                && hitUp
                && hitRight
                )
        {
            _renderer.flipX = false;
            _renderer.sprite = _SideMiddle;
            _ID = BlockType.SIDE_MIDDLE_LEFT;
        }
        //side middle right
        else if (!hitRight
               && hitDown
               && hitUp
               && hitLeft
               )
        {
            _renderer.flipX = true;
            _renderer.sprite = _SideMiddle;
            _ID = BlockType.SIDE_MIDDLE_RIGHT;
        }
        //middle middle
        else if (hitRight
            && hitLeft
            && hitDown
            && hitUp
            )
        {
            _renderer.flipX = false;
            _renderer.sprite = _MiddleMiddle;
            _ID = BlockType.MIDDLE_MIDDLE;
        }
        //bottom corner left
        else if (hitRight
            && hitUp
            && !hitLeft
            && !hitDown
            )
        {
            _renderer.flipX = false;
            _renderer.sprite = _BottomCorner;
            _ID = BlockType.BOTTOM_LEFT;
        }
        //bottom corner right
        else if (hitLeft
            && hitUp
            && !hitRight
            && !hitDown
            )
        {
            _renderer.flipX = true;
            _renderer.sprite = _BottomCorner;
            _ID = BlockType.BOTTOM_RIGHT;
        }
        //bottom center
        else if (hitUp
            && hitLeft
            && hitRight
            && !hitDown
           )
        {
            _renderer.flipX = false;
            _renderer.sprite = _BottomMiddle;
            _ID = BlockType.BOTTOM_MIDDLE;
        }
        
    }

    private void UpdateSurroundings()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.zero, _mask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), Vector2.zero, _mask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position + new Vector3(0, -1, 0), Vector2.zero, _mask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(-1, 0, 0), Vector2.zero, _mask);

        if (hitUp)
        {
            AutoTilingScript scr = hitUp.collider.gameObject.GetComponent<AutoTilingScript>();
            if (scr)
            {
                scr.CheckSurroundings();
            }
        }
        if (hitDown)
        {
            AutoTilingScript scr = hitDown.collider.gameObject.GetComponent<AutoTilingScript>();
            if (scr)
            {
                scr.CheckSurroundings();
            }
        }

        if (hitLeft)
        {
            AutoTilingScript scr = hitLeft.collider.gameObject.GetComponent<AutoTilingScript>();
            if (scr)
            {
                scr.CheckSurroundings();
            }
        }
        if (hitRight)
        {
            AutoTilingScript scr = hitRight.collider.gameObject.GetComponent<AutoTilingScript>();
            if (scr)
            {
                scr.CheckSurroundings();
            }
        }
    }

    private void OnDestroy()
    {
        UpdateSurroundings();
    }

    private bool CheckRaycastHit(RaycastHit2D hit)
    {
        if(hit)
        {
            return hit.collider.tag == "Block";
        }
        return false;
    }
}
