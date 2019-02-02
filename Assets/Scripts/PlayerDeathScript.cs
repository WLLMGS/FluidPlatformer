using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour {

    [SerializeField] private GameObject _brokenPlayer;
    private PlayerMovement _movement;


    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void ReplaceWithBrokenPlayer()
    {
        var bp = Instantiate(_brokenPlayer, transform.position, Quaternion.identity);
        PlayerFollowCameraScript.Player = bp;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sawblade"
            || collision.tag == "SawbladeProjectile")
        {
            //notify gameplay manager
            GameplayManager.Instance.NotifyPlayerDeath();
            //create broken player
            ReplaceWithBrokenPlayer();
            //destroy current player
            Destroy(gameObject);
        }
        else if(collision.tag == "Spikes")
        {
            //notify of death
            GameplayManager.Instance.NotifyPlayerDeath();
            //replace with broken player
            ReplaceWithBrokenPlayer();
            //destroy current player
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "SlimeHitbox")
        {
            _movement.DoJump = true;
            Debug.Log("HIT");
            Destroy(collision.gameObject.transform.parent.gameObject);

            //get parent slime obj
            var slime = collision.transform.parent.gameObject;
            //get collider
            var collider = slime.GetComponent<BoxCollider2D>();
            //get behavior script
            var scr = slime.GetComponent<SlimeBehaviorScript>();
            //disable collider
            if (collider) collider.enabled = false;
            //do death event
            if (scr) scr.Die();
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            var scr = collision.gameObject.GetComponent<SlimeBehaviorScript>();

            if(scr
                && scr.IsDangerous)
            {
                //notify of death
                GameplayManager.Instance.NotifyPlayerDeath();
                //replace with broken player
                ReplaceWithBrokenPlayer();
                //destroy current player
                Destroy(gameObject);
            }
            
        }
    }
}
