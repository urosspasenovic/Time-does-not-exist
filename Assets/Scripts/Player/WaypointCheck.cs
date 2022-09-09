using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCheck : MonoBehaviour
{
    Movement player;
    private void Awake()
    {
        player = GetComponentInParent<Movement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            player.StopMoving();
        }
    }
}
