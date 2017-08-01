﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAreaDetector : MonoBehaviour
{   
    [Tooltip("number of seconds that must pass without collision before heli can be called")]
    public float CollisionTimeThreshold = 1f; 
    private float lastCollisionSeconds;    
    private bool clearAreaFound;
    private int collisionCount;
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

	// Update is called once per frame
	void Update () {
        lastCollisionSeconds += Time.deltaTime;

        Debug.Log("Collisions: " + collisionCount);
        
	    if (collisionCount > 0)
	        lastCollisionSeconds = 0;

	    if (player.IsDead)
	        return;

	    if (!(lastCollisionSeconds >= CollisionTimeThreshold && Time.realtimeSinceStartup > 10f && collisionCount == 0)|| clearAreaFound)
            return;

        //clear area has been found
	    clearAreaFound = true;
        SendMessageUpwards("OnFindClearArea");	      
	}

    void OnTriggerEnter(Collider collider)
    {        
        //todo: add collision checks for gun & bullets
        if (collider.tag != "Player" && collider.name != "Ceiling")
        {            
            collisionCount++;
            //Debug.Log("Collided with " + collider.name + " Collisions: " + collisionCount);
        }            
    }

    void OnTriggerExit()
    {
        collisionCount--;
    }
}
