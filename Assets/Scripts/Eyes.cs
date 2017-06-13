using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{

    private Camera eyes;
    private float defaultFov; //field of view

	// Use this for initialization
	void Start ()
	{
	    eyes = GetComponent<Camera>();
	    defaultFov = eyes.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButton("Zoom"))     
	        eyes.fieldOfView = defaultFov / 1.5f;
	        
	    else	    
	        eyes.fieldOfView = defaultFov;                   
	}
}
