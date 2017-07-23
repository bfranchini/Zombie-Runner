﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{

    private Camera playerCamera;
    private float defaultFov; //field of view
    private float zoomMultiplier = 1.5f; //how much to zoom in
    private float zoomedFieldOfView;
    public float zoomSpeed = 5f;

	// Use this for initialization
	void Start ()
	{
	    playerCamera = GetComponent<Camera>();
	    defaultFov = playerCamera.fieldOfView;
	    zoomedFieldOfView = defaultFov / zoomMultiplier;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var fov = playerCamera.fieldOfView;

	    if (Input.GetButton("Zoom"))
	    {
	        if (fov > zoomedFieldOfView)
	        {
	            playerCamera.fieldOfView = 
                    Mathf.Clamp(fov - zoomedFieldOfView * Time.deltaTime * zoomSpeed, zoomedFieldOfView, defaultFov);
	        }

            return;
        }

	    if (fov < defaultFov)
	    {
	         playerCamera.fieldOfView = 
                Mathf.Clamp(fov + defaultFov * Time.deltaTime * zoomSpeed, 0, defaultFov); 	        
	    }	        
	}
}
