using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour {

    [SerializeField] private GameObject _brokenPlayer;

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
    }
}
