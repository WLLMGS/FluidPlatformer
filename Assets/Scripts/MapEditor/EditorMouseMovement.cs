using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMouseMovement : MonoBehaviour
{

    private void Update()
    {
        HandleMouseMovement();
    }

    private void HandleMouseMovement()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pos.x -= 0.5f;
        pos.y -= 0.5f;

        transform.position = new Vector3(Mathf.Ceil(pos.x), Mathf.Ceil(pos.y), transform.position.z);
    }

    
}
