using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private List<string> _tagsToIgnore = new List<string>();

    private float _rayLengthHorz = 0.75f;
    private float _rayLenghtVert = 0.65f;

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
    LayerMask mask;
    private void Start()
    {
        mask = 1 << 9;
    }

    private void Update()
    {
        //DOWN
        CheckCollisionsDown();
        CheckCollisionsLeft();
        CheckCollisionsRight();
        // CheckCollisionsUp();
    }

    void CheckCollisionsDown()
    {
        //DEBUG
        Debug.DrawRay(transform.position, new Vector3(0, -_rayLenghtVert, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0.25f, 0, 0), new Vector3(0, -_rayLenghtVert, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(-0.25f, 0, 0), new Vector3(0, -_rayLenghtVert, 0), Color.green);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), _rayLenghtVert, mask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0.25f, 0, 0), new Vector2(0, -1), _rayLenghtVert, mask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(-0.25f, 0, 0), new Vector2(0, -1), _rayLenghtVert, mask);

        //if(DoesCollide(hit, hit2, hit3))
        if(hit || hit2 || hit3)
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
        Debug.DrawRay(transform.position + new Vector3(0, -0.25f, 0), new Vector3(_rayLengthHorz, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), new Vector3(_rayLengthHorz, 0), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0), _rayLengthHorz, mask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0, 0.25f, 0), new Vector2(1, 0), _rayLengthHorz, mask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(0, -0.25f, 0), new Vector2(1, 0), _rayLengthHorz, mask);

        if (DoesCollide(hit, hit2, hit3))
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
        Debug.DrawRay(transform.position + new Vector3(0, -0.25f, 0), new Vector3(-_rayLengthHorz, 0), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), new Vector3(-_rayLengthHorz, 0), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1, 0), _rayLengthHorz, mask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0, 0.25f, 0), new Vector2(-1, 0), _rayLengthHorz, mask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(0, -0.25f, 0), new Vector2(-1, 0), _rayLengthHorz, mask);

        if (DoesCollide(hit, hit2, hit3))
        {
            _Left = true;
        }
        else
        {
            _Left = false;
        }
    }

    bool DoesCollide(RaycastHit2D hit, RaycastHit2D hit2, RaycastHit2D hit3)
    {
        //bool match1 = false;
        //bool match2 = false;
        //bool match3 = false;

        //foreach (string tag in _tagsToIgnore)
        //{
        //    if ((hit) && hit.collider.tag == tag) match1 = true;
        //    if ((hit2) && hit2.collider.tag == tag) match2 = true;
        //    if ((hit3) && hit3.collider.tag == tag) match3 = true;
        //}

        //if (!hit) match1 = true;
        //if (!hit2) match2 = true;
        //if (!hit3) match3 = true;

        //return (!match1 || !match2 || !match3);

        bool match1 = false;
        bool match2 = false;
        bool match3 = false;

        if ((hit) && hit.collider.tag == "Block") match1 = true;
        if ((hit2) && hit2.collider.tag == "Block") match2 = true;
        if ((hit3) && hit3.collider.tag == "Block") match3 = true;

        return (match1 || match2 || match3);
    }
}
