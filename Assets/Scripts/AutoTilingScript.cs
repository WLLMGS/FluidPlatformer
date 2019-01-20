using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTilingScript : MonoBehaviour
{
    [SerializeField] private Sprite _TopCorner;
    [SerializeField] private Sprite _TopMiddle;
    [SerializeField] private Sprite _SideMiddle;
    [SerializeField] private Sprite _BottomCorner;
    [SerializeField] private Sprite _BottomMiddle;
    [SerializeField] private Sprite _MiddleMiddle;

    private SpriteRenderer _renderer;
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

       if(_renderer) CheckSurroundings();
        UpdateSurroundings();
    }


    public void CheckSurroundings()
    {
        _renderer = GetComponent<SpriteRenderer>();

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.zero);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), Vector2.zero);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position + new Vector3(0, -1, 0), Vector2.zero);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(-1, 0, 0), Vector2.zero);

        //top left
        if (!hitUp
            && hitDown
            && !hitLeft
            && hitRight)
        {
            _renderer.flipX = false;
            _renderer.sprite = _TopCorner;
        }
        //top middle
        else if (!hitUp
            && hitDown
            && hitLeft
            && hitRight)
        {

            _renderer.flipX = false;
            _renderer.sprite = _TopMiddle;
        }
        //top right
        else if (!hitRight
            && !hitUp
            && hitDown
            && hitLeft)
        {
            _renderer.flipX = true;
            _renderer.sprite = _TopCorner;
        }
        //side middle left
        else if (!hitLeft
                && hitDown
                && hitUp
                && hitRight)
        {
            _renderer.flipX = false;
            _renderer.sprite = _SideMiddle;
        }
        //side middle right
        else if (!hitRight
               && hitDown
               && hitUp
               && hitLeft)
        {
            _renderer.flipX = true;
            _renderer.sprite = _SideMiddle;
        }
        //middle middle
        else if(hitRight
            && hitLeft
            && hitDown
            && hitUp)
        {
            _renderer.flipX = false;
            _renderer.sprite = _MiddleMiddle;
        }
        //bottom corner left
        else if(hitRight
            && hitUp
            && !hitLeft
            && !hitDown)
        {
            _renderer.flipX = false;
            _renderer.sprite = _BottomCorner;
        }
        //bottom corner right
        else if (hitLeft
            && hitUp
            && !hitRight
            && !hitDown)
        {
            _renderer.flipX = true;
            _renderer.sprite = _BottomCorner;
        }
        //bottom center
        else if(hitUp
            && hitLeft
            && hitRight
            && !hitDown)
        {
            _renderer.flipX = false;
            _renderer.sprite = _BottomMiddle;
        }
    }

    private void UpdateSurroundings()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.zero);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), Vector2.zero);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position + new Vector3(0, -1, 0), Vector2.zero);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(-1, 0, 0), Vector2.zero);

        if(hitUp)
        {
            AutoTilingScript scr = hitUp.collider.gameObject.GetComponent<AutoTilingScript>();
            if (scr)
            {
                scr.CheckSurroundings();
            }
        }
         if(hitDown)
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
}
