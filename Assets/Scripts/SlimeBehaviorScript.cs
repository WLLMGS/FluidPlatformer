using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SlimeBehaviorScript : MonoBehaviour {

    [SerializeField] private GameObject _deathParticle;
    private Rigidbody2D _rigid;
    private bool _IsDangerous = true;

    private SelectorNode _rootNode;

    private float _movespeed = 2.5f;
    private bool _isGoingRight = true;
    private LayerMask _mask = 1 << 9;
    public bool IsDangerous
    {
        get { return _IsDangerous; }
    }

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();

        _rootNode = new SelectorNode(
            new SequenceNode(
                new ConditionNode(IsGoingRight),
                new ActionNode(CheckRight)
                ),
            
            new SequenceNode(
                new ConditionNode(IsGoingRight),
                new ActionNode(GoRight)
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


    private NodeState CheckRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0), 2.0f, _mask);
        if(hit)
        {
            _isGoingRight = false;
            return NodeState.Failure;
        }
        return NodeState.Failure;
    }

    private NodeState GoRight()
    {
        _rigid.velocity = new Vector2(_movespeed, _rigid.velocity.y);
        return NodeState.Success;
    }

}
