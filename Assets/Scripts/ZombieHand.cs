using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHand : MonoBehaviour
{
    private Zombie zombie;
    private UI ui;
    
    void Start()
    {
        zombie = GetComponentInParent<Zombie>();
        ui = FindObjectOfType<UI>();
    }

    void OnTriggerEnter(Collider collider)
    {
        var player = collider.GetComponent<Player>();

        if (player == null)
            return;

        player.Damage(zombie.DamagePerHit);        
        ui.GetComponent<Animator>().SetTrigger("PlayerHit");
    }
}
