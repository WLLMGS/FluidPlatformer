using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCollisions : MonoBehaviour
{

    private float _rayLengthHorz = 0.5f;
    private float _rayLenghtVert = 0.75f;


    private bool _Down = false;
    private bool _Up = false;
    private bool _Right = false;
    private bool _Left = false;

    public bool Down
    {
        get { return _Down; }
    }

    public bool Up
    {
        get { return _Up; }
    }

    public bool Right
    {
        get { return _Right; }
    }
    public bool Left
    {
        get { return _Left; }
    }

    private void Update()
    {
        //DOWN
        CheckCollisionsDown();
        CheckCollisionsLeft();
        CheckCollisionsRight();
        CheckCollisionsUp();
    }

    void CheckCollisionsDown()
    {
        //DEBUG
        Debug.DrawRay(transform.position, new Vector3(0, -_rayLenghtVert, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0.25f, 0, 0), new Vector3(0, -_rayLenghtVert, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(-0.25f, 0, 0), new Vector3(0, -_rayLenghtVert, 0), Color.green);



        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), _rayLenghtVert);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0.25f, 0, 0), new Vector2(0, -1), _rayLenghtVert);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(-0.25f, 0, 0), new Vector2(0, -1), _rayLenghtVert);

        if (hit || hit2 || hit3)
        {
            _Down = true;
        }
        else
        {
            _Down = false;
        }
    }

    void CheckCollisionsUp()
    {
        //DEBUG
        Debug.DrawRay(transform.position, new Vector3(0, _rayLenghtVert, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0.25f, 0, 0), new Vector3(0, _rayLenghtVert, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(-0.25f, 0, 0), new Vector3(0, _rayLenghtVert, 0), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, 1), _rayLenghtVert);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0.25f, 0, 0), new Vector2(0, 1), _rayLenghtVert);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(-0.25f, 0, 0), new Vector2(0, 1), _rayLenghtVert);

        if (hit || hit2 || hit3)
        {
            _Up = true;
        }
        else
        {
            _Up = false;
        }
    }

    void CheckCollisionsRight()
    {
        //DEBUG
        Debug.DrawRay(transform.position, new Vector3(_rayLengthHorz, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, 0.25f, 0), new Vector3(_rayLengthHorz, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, -0.25f, 0), new Vector3(_rayLengthHorz, 0), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0), _rayLengthHorz);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0, 0.25f, 0), new Vector2(1, 0), _rayLengthHorz);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(0, -0.25f, 0), new Vector2(1, 0), _rayLengthHorz);

        if (hit || hit2 || hit3)
        {
            _Right = true;
        }
        else
        {
            _Right = false;
        }
    }

    void CheckCollisionsLeft()
    {
        //DEBUG
        Debug.DrawRay(transform.position, new Vector3(-_rayLengthHorz, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, 0.25f, 0), new Vector3(-_rayLengthHorz, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, -0.25f, 0), new Vector3(-_rayLengthHorz, 0), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1, 0), _rayLengthHorz);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0, 0.25f, 0), new Vector2(-1, 0), _rayLengthHorz);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(0, -0.25f, 0), new Vector2(-1, 0), _rayLengthHorz);

        if (hit || hit2 || hit3)
        {
            _Left = true;
        }
        else
        {
            _Left = false;
        }
    }
}
