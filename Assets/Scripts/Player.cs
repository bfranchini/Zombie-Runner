using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private GameObject spawnParent;
    private Transform[] spawnPoints;
    public bool Respawn;

	// Use this for initialization
	void Start () {
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
}
