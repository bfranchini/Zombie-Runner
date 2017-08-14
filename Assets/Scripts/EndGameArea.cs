using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameArea : MonoBehaviour
{
    private Helicopter helicopter;
    private UI ui;
    private Player player;

    void Start()
    {
        helicopter = GetComponentInParent<Helicopter>();
        ui = FindObjectOfType<UI>();
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>() != null && helicopter.Landed)
        {
            ui.SetNotificationText("Congratulations, you escaped the zombies!");
            ui.DisableCrosshair();
            ui.EnableBackToMenuButton();
            player.EndGame();
            StopAllAudio();
            Time.timeScale = 0;
            Debug.Log("End game!!!!!!");
        }
    }

    private void StopAllAudio()
    {
        var audioSources = FindObjectsOfType<AudioSource>();

        foreach (var source in audioSources)
            source.Stop();
    }
}
