using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHand : MonoBehaviour
{
    private Zombie zombie;
    // Use this for initialization
    void Start()
    {
        zombie = GetComponentInParent<Zombie>();
    }

    void OnTriggerEnter(Collider collider)
    {
        var player = collider.GetComponent<Player>();

        if (player == null)
            return;

        player.Damage(zombie.DamagePerHit);        
    }
}
