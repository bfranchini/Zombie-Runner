using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int AmmoCount = 30;
    public AudioClip audioClip;   

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>() == null)
            return;

        var gun = FindObjectOfType<Gun>();

        if (gun.AddAmmo(AmmoCount))
        {          
            AudioSource.PlayClipAtPoint(audioClip, collider.transform.position);
            Destroy(gameObject);
        }
    }
}
