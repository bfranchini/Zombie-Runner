using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerVoice : MonoBehaviour
{

    public AudioClip WhatHappened;
    public AudioClip GoodLandingArea;
    private AudioSource audioSource;
    private UI ui;

    public void OnFindClearArea()
    {
        print(name + " OnFindClearArea");
       // audioSource.clip = GoodLandingArea;
        //audioSource.Play();

        //wait until good landing area is finished playing before calling heli
//        Invoke("CallHeli", GoodLandingArea.length + 1f);            
            Invoke("CallHeli", 1);
    }

    public void CallHeli()
    {
        SendMessageUpwards("OnMakeInitialHeliCall");
    }
}
