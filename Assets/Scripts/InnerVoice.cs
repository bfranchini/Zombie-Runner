using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerVoice : MonoBehaviour
{

    public AudioClip WhatHappened;
    public AudioClip GoodLandingArea;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	    audioSource.clip = WhatHappened;
        audioSource.Play();
	}

    public void OnFindClearArea()
    {
        print(name + " OnFindClearArea");
        audioSource.clip = GoodLandingArea;
        audioSource.Play();

        //wait until good landing area is finished playing before calling heli
        Invoke("CallHeli", GoodLandingArea.length + 1f);
    }

    public void CallHeli()
    {
        SendMessageUpwards("OnMakeInitialHeliCall");
    }
}
