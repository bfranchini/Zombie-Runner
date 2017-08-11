using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAreaDetector : MonoBehaviour
{
    [Tooltip("number of seconds that must pass without collision before heli can be called")]
    
    private bool clearAreaFound;
    private int collisionCount;
    private UI ui;

    void Start()
    {
        ui = FindObjectOfType<UI>();
    }

    // Update is called once per frame    

    public bool CallHelicopter()
    {
        if (collisionCount == 0 || clearAreaFound)
        {
            //clear area has been found
            clearAreaFound = true;
            SendMessageUpwards("OnFindClearArea");
            return true;
        }

        ui.SetNotificationText("The helicopter can't land here! Find a clearer area.");
        return false;
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
