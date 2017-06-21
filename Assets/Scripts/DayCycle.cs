using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {

    //minutes per second. How many minutes are in one second of the game
    [Tooltip("Number of minutes per second that pass")]
    public float MinutesPerSecond = 60f;

    private Transform transform;       
    private float degreesPerFrame;

    // Use this for initialization
    void Start ()
	{
	    transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{             
	    degreesPerFrame = MinutesPerSecond * Time.deltaTime * 360 / 1440; //1440 minutes in a day	    	    
	    transform.Rotate(degreesPerFrame, 0, 0, Space.Self);
	}
}
