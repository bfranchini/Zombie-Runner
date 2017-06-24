using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAreaDetector : MonoBehaviour
{   
    [Tooltip("number of seconds that must pass without collision before heli can be called")]
    public float CollisionTimeThreshold = 1f; 
    private float lastCollisionSeconds;    
    private bool clearAreaFound;
	
	// Update is called once per frame
	void Update () {
        lastCollisionSeconds += Time.deltaTime;

	    if (!(lastCollisionSeconds >= CollisionTimeThreshold && Time.realtimeSinceStartup > 10f)|| clearAreaFound)
            return;

        //clear area has been found
	    clearAreaFound = true;
        SendMessageUpwards("OnFindClearArea");	      
	}

    void OnTriggerStay(Collider collider)
    {       
        Debug.Log("Collided with " + collider.name);

        if(collider.tag != "Player")
            lastCollisionSeconds = 0;
    }
}
