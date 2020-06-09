using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GroundUpDown")
        {
            player.OnGround(other.gameObject.layer);
            var script = other.GetComponent<GroundUpDown>();
            if (script != null)
            {
                script.SetPlayer(player);
            }
            return;
        }
        else
        {
            player.OnGround(other.gameObject.layer);
            var script = other.GetComponent<GroundMove>();
            if (script != null)
            {
                script.SetPlayer(player);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "GroundUpDown")
        {
            var script = other.GetComponent<GroundUpDown>();
            if (script != null)
            {
                script.SetPlayer(null);
            }
        }
        else
        {
            var script = other.GetComponent<GroundMove>();
            if (script != null)
            {
                script.SetPlayer(null);
            }
        }

    }
}
