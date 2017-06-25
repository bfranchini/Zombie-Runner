using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{    
    //todo: make private
    private bool called;
    public float Speed = 50f; //50f = 5 minutes
    private AudioSource audioSource;
    private Rigidbody rigidbody;
    private GameObject landingArea;
    private float travelTime;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody>();
	}

    void Update()
    {
        if (called)
        {
            var step = Speed * Time.deltaTime;
            travelTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, landingArea.transform.position, step);
        }
    }

    public void OnDispatchHelicopter()
    {
        if (!called)
        {               
            //find landing area
            landingArea = GameObject.FindGameObjectWithTag("LandingArea");

            if (landingArea == null)
            {
                Debug.LogError("Could not find landing area");
                return;
            }

            called = true;
            Debug.Log("Helicopter called");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "LandingArea")
        {
            Debug.Log("total flight seconds: " + travelTime);
        }
    }
}
