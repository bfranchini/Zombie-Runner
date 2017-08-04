using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSystem : MonoBehaviour
{
    public AudioClip InitialHeliCall;
    public AudioClip InitialCallReply;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	    var musicManager = FindObjectOfType<MusicManager>();

        if(musicManager != null)
            musicManager.StopMusic();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMakeInitialHeliCall()
    {
        audioSource.clip = InitialHeliCall;
        audioSource.Play();

        Invoke("OnDispatchReply", InitialHeliCall.length + 1);
    }

    private void OnDispatchReply()
    {
        audioSource.clip = InitialCallReply;
        audioSource.Play();
        BroadcastMessage("OnDispatchHelicopter");
    }
}
