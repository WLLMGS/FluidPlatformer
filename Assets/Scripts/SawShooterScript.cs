using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawShooterScript : MonoBehaviour {

    [SerializeField] private GameObject _sawPrefab;
    private bool _isReadyToShoot = true;
    private float _cooldown = 1.0f;

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


        if (scr != null)
        {
            float angle = transform.rotation.eulerAngles.z;
            Vector2 direction = Vector2.zero;
            if (angle == 0.0f) direction = new Vector2(-1, 0);
            else if (angle == 90.0f) direction = new Vector2(0, -1);
            else if (angle == 180.0f) direction = new Vector2(1, 0);
            else if (angle == 270.0f) direction = new Vector2(0, 1);

            scr.Direction = direction;
        }
        
        //start cooldown
        Invoke("ResetCooldown", _cooldown);
    }

    private void ResetCooldown()
    {
        _isReadyToShoot = true;
    }

}
