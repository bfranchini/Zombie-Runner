using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        var player = collider.GetComponent<Player>();

        if (player != null)
        {
            var health = player.GetComponent<Health>();
            health.TakeDamage(health.GetCurrentHealth());
        }            
    }
}
