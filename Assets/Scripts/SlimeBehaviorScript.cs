using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviorScript : MonoBehaviour {

    [SerializeField] private GameObject _deathParticle;
    private bool _IsDangerous = true;
    
    public bool IsDangerous
    {
        get { return _IsDangerous; }
    }

    public void Die()
    {
        _IsDangerous = false;
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
    }
}
