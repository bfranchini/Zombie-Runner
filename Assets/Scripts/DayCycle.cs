﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {

    //minutes per second. How many minutes are in one second of the game
    [Tooltip("Number of minutes per second that pass")]
    public float MinutesPerSecond = 1f;
      
    private float degreesPerFrame;

	// Update is called once per frame
	void Update ()
	{             
	    degreesPerFrame = MinutesPerSecond * Time.deltaTime * 360 / 1440; //1440 minutes in a day	    	    
	    transform.Rotate(degreesPerFrame, 0, 0, Space.Self);
	}
}
