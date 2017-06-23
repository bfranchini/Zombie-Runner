using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Helicopter helicopter;
    public AudioClip WhatHappened;
    private Transform[] spawnPoints;    
    private bool Respawn; //used for testing respawn points(make public and toggle)
    private AudioSource innerVoice;    

    // Use this for initialization
    void Start ()
    {
        var audioSources = GetComponents<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            if (audioSource.priority == 1)
                innerVoice = audioSource;
        }

        innerVoice.clip = WhatHappened;
        innerVoice.Play();

        var spawnParent = GameObject.Find("Player Spawn Points");

        if (spawnParent == null)
        {
            Debug.Log("Could not find spawn point parent");
            return;
        }

        spawnPoints = spawnParent.GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Respawn)
            ReSpawn();                
	}

    private void ReSpawn()
    {
        Respawn = false;

        if (spawnPoints.Length == 0)
            return; 

        var respawnPoint = spawnPoints[Random.Range(1, spawnPoints.Length)];

        transform.position = respawnPoint.position;
    }

    private void OnFindClearArea()
    {
        Debug.Log("Found clear area in player");
        helicopter.Call();
        //deploy flare
        //start spawning zombies
    }
}
