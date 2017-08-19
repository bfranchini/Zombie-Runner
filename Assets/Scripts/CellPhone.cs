using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhone : MonoBehaviour
{
    public AudioClip audioClip;
    private UI ui;
    private ClearAreaDetector clearAreaDetector;

    void Start()
    {
        ui = FindObjectOfType<UI>();
        clearAreaDetector = FindObjectOfType<ClearAreaDetector>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        var player = collider.GetComponent<Player>();

        if (player == null)
            return;

        player.SetPhone(true);
        ui.SetNotificationText("Press H to call the helicopter");

        AudioSource.PlayClipAtPoint(audioClip, collider.transform.position);
        clearAreaDetector.DecrementCollision(1);
        Destroy(gameObject);
    }
}
