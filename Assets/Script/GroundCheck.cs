using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        player.OnGround(other.gameObject.layer);
    }
}
