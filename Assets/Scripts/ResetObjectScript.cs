using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectScript : MonoBehaviour {

    private Vector2 _startPos;

    private List<Collider2D> _colliders = new List<Collider2D>();

    private void Start()
    {
        _startPos = transform.position;

        //get all colliders in game object and in children and add them to the list
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders) _colliders.Add(collider);
        colliders = GetComponentsInChildren<Collider2D>();
        foreach (var collider in colliders) _colliders.Add(collider);
    }

    public void Reset()
    {
        //set active again
        gameObject.SetActive(true);
        //reset original pos
        transform.position = _startPos;
        //reactivate colliders
        foreach (var collider in _colliders) collider.enabled = true;

        //get slime comp
        var slimecomp = GetComponent<SlimeBehaviorScript>();
        if (slimecomp) slimecomp.IsDangerous = true;
    }
}
