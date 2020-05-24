using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        player.OnGround(other.gameObject.layer);
        var script = other.GetComponent<GroundMove>();
        if (script != null)
        {
            script.SetPlayer(player);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var script = other.GetComponent<GroundMove>();
        if (script != null)
        {
            script.SetPlayer(null);
        }
    }
}
