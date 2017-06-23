using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public AudioClip callSound;
    //todo: make private
    public bool called = false;
    private AudioSource audioSource;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	    rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void Call()
    {
        if (!called)
        {
            called = true;
            Debug.Log("Helicopter called");
            audioSource.clip = callSound;
            audioSource.Play();
            rigidbody.velocity = new Vector3(0, 0, 50f);
        }
    }
}
