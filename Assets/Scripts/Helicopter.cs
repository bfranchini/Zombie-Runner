using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    //todo: make private
    private bool called;
    private bool arrived;
    public float TravelSpeed = 30f; //30f = 5 minutes(9000 meters / 300 seconds)
    public float descentSpeed = 50f;

    private AudioSource audioSource;    
    private GameObject landingArea;
    private GameObject landingPoint;
    private float travelTime;

    void Update()
    {
        if (called && !arrived)
        {
            var step = TravelSpeed * Time.deltaTime;
            travelTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, landingPoint.transform.position, step);
            return;
        }

        if(!arrived)
            return;        

        var descentStep = descentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, landingArea.transform.position, descentStep);
    }

    public void OnDispatchHelicopter()
    {
        if (!called)
        {
            //find landing area & landing point
            landingArea = GameObject.FindGameObjectWithTag("LandingArea");
            landingPoint = GameObject.FindGameObjectWithTag("LandingPoint");

            if (landingPoint == null)
            {
                Debug.LogError("Could not find landing point");
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
            arrived = true;
            Debug.Log("total flight seconds: " + travelTime);
        }
    }
}
