using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawShooterScript : MonoBehaviour {

    [SerializeField] private GameObject _sawPrefab;
    private bool _isReadyToShoot = true;
    private float _cooldown = 1.5f;

    private void Update()
    {
        if(_isReadyToShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //set ready to shoot to false
        _isReadyToShoot = false;
        //spawn sawblade
        var projectile = Instantiate(_sawPrefab, transform.position, Quaternion.identity);
        SawbladeProjectileScript scr = projectile.GetComponent<SawbladeProjectileScript>();
        if (scr != null) scr.Direction = new Vector2(-1, 0);
        //start cooldown
        Invoke("ResetCooldown", _cooldown);
    }

    private void ResetCooldown()
    {
        _isReadyToShoot = true;
    }

}
