using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    public int HealthAmount = 30;
    public AudioClip AudioClip;
    
    public void OnTriggerEnter(Collider collider)
    {       
        if(collider.GetComponent<Player>() == null)
            return;

        var playerHealth = collider.GetComponent<Health>();

        if (playerHealth.AddHealth(HealthAmount))
        {
            AudioSource.PlayClipAtPoint(AudioClip, collider.transform.position);
            Destroy(gameObject);
        }
    }
}
