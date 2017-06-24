using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{    
    //todo: make private
    public bool called = false;
    private AudioSource audioSource;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody>();
	}

    public void OnDispatchHelicopter()
    {
        if (!called)
        {
            called = true;
            Debug.Log("Helicopter called");     
            rigidbody.velocity = new Vector3(0, 0, 50f);
        }
    }
}
