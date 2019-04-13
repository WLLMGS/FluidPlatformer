using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SlimeBehaviorScript : MonoBehaviour
{

    [SerializeField] private GameObject _deathParticle;
    private Rigidbody2D _rigid;
    private SpriteRenderer _renderer;
    private bool _IsDangerous = true;

    private SelectorNode _rootNode;

    private float _movespeed = 2.5f;
    private bool _isGoingRight = false;
    private LayerMask _mask = 1 << 9;
    private float _lookDistance = 0.85f;

    public bool IsDangerous
    {
        get { return _IsDangerous; }
        set { _IsDangerous = value; }
    }

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        _rootNode = new SelectorNode(

            //check if right or left
            new SequenceNode(
                new ConditionNode(IsGoingRight),
                new ActionNode(CheckRight)
                ),
            new SequenceNode(
                new ConditionNode(IsGoingRight),
                new ActionNode(CheckRightDiagonal)
                ),
            new SequenceNode(
                new ConditionNode(IsGoingLeft),
                new ActionNode(CheckLeft)
                ),
            new SequenceNode(
                new ConditionNode(IsGoingLeft),
                new ActionNode(CheckLeftDiagonal)
                ),
            new SequenceNode(
                new ActionNode(Move)
                )
            );
    }

    private void Update()
    {
        _rootNode.Tick();
    }

    public void Die()
    {
        _IsDangerous = false;
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    //===== AI =====

    private bool IsGoingRight()
    {
        return _isGoingRight;
    }

    private bool IsGoingLeft()
    {
        return !_isGoingRight;
    }

    private NodeState CheckRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0), _lookDistance, _mask);
        if (hit)
        {
            _isGoingRight = false;
            _renderer.flipX = false;
            return NodeState.Continue;
        }
        return NodeState.Failure;
    }

    private NodeState CheckLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1, 0), _lookDistance, _mask);
        if (hit)
        {
            _isGoingRight = true;
            _renderer.flipX = true;
            return NodeState.Continue;
        }
        return NodeState.Failure;
    }

    private NodeState CheckRightDiagonal()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, -0.5f), _lookDistance * 2.0f, _mask);
        Debug.DrawRay(transform.position, new Vector2(1, -0.5f), Color.green);
        if (!hit)
        {
            _isGoingRight = false;
            _renderer.flipX = false;
            return NodeState.Continue;
        }
        return NodeState.Failure;
    }
    private NodeState CheckLeftDiagonal()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1, -0.5f), _lookDistance * 2.0f, _mask);
        Debug.DrawRay(transform.position, new Vector2(-1, -0.5f), Color.green);
        if (!hit)
        {
            _isGoingRight = true;
            _renderer.flipX = true;
            return NodeState.Continue;
        }
        return NodeState.Failure;
    }
    private NodeState Move()
    {
        float direction = (_isGoingRight) ? 1.0f : -1.0f;

        _rigid.velocity = new Vector2(_movespeed * direction, _rigid.velocity.y);
        return NodeState.Success;
    }

}
