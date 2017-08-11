using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhone : MonoBehaviour
{
    public AudioClip audioClip;
    private UI ui;

    void Start()
    {
        ui = FindObjectOfType<UI>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        var player = collider.GetComponent<Player>();

        if (player == null)
            return;

        player.SetPhone(true);
        ui.SetNotificationText("Press H to call the helicopter");

        AudioSource.PlayClipAtPoint(audioClip, collider.transform.position);
        Destroy(gameObject);
    }
}
