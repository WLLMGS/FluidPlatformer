using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCameraScript : MonoBehaviour
{

    private static GameObject _followObj;

    public static GameObject Player
    {
        get { return _followObj; }
        set { _followObj = value; }
    }

    private Animator _animator;
    private float _ZValue = -50.0f;

    private void Update()
    {
        HandlePlayerFollow();
    }

    private void HandlePlayerFollow()
    {
        if (_followObj)
        {
            Vector2 playerPos = _followObj.transform.position;
            Vector3 target = new Vector3(playerPos.x, playerPos.y, _ZValue);
            float distance = Vector2.Distance(transform.position, playerPos);

            //check if camera is close enough to player

            if (distance > 2.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, 20.0f * Time.deltaTime);
            }
            else
            {
                if (!PlayerMovement.CanMove
                    && _followObj.tag == "Player")
                {
                    PlayerMovement.CanMove = true;
                }
                    transform.position = target;
            }
        }
    }
}
