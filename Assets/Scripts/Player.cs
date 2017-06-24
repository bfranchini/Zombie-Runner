using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{     
    private Transform[] spawnPoints;    
    private bool Respawn; //used for testing respawn points(make public and toggle)  
    public GameObject LandingAreaPrefab;

    // Use this for initialization
    void Start ()
    {
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
        Debug.Log("Found clear area");
        Invoke("DropFlare", 3f);     
    }

    private void DropFlare()
    {
        Debug.Log("Dropped Flare");
        var flarePosition = new Vector3(transform.position.x, LandingAreaPrefab.transform.position.y, transform.position.z);
        Instantiate(LandingAreaPrefab, flarePosition, transform.rotation);
    }
}
