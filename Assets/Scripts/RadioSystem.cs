using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSystem : MonoBehaviour
{
    public AudioClip InitialHeliCall;
    public AudioClip InitialCallReply;
    private AudioSource audioSource;
    private UI ui;

	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	    var musicManager = FindObjectOfType<MusicManager>();

        if(musicManager != null)
            musicManager.StopMusic();

	    ui = FindObjectOfType<UI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMakeInitialHeliCall()
    {
        //audioSource.clip = InitialHeliCall;
        //audioSource.Play();
        //ui.SetNotificationText("");
        OnDispatchReply();
        //Invoke("OnDispatchReply", InitialHeliCall.length + 1);
    }

    private void OnDispatchReply()
    {
        ui.SetNotificationText("The helicopter has been called and will arrive in 30 seconds");
        BroadcastMessage("OnDispatchHelicopter");
    }
}
