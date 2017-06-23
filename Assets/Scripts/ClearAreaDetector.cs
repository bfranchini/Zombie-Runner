using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAreaDetector : MonoBehaviour
{   
    [Tooltip("number of seconds that must pass without collision before heli can be called")]
    public float CollisionTimeThreshold = 1f;
    public AudioClip FoundAreaAudioClip;    

    private float lastCollisionSeconds;
    private AudioSource audioSource;
    private bool clearAreaFound;

	// Use this for initialization
	void Start ()
	{
	    audioSource = transform.parent.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        lastCollisionSeconds += Time.deltaTime;

	    if (!(lastCollisionSeconds >= CollisionTimeThreshold && Time.realtimeSinceStartup > 10f)|| clearAreaFound)
            return;

        //clear area has been found
	    clearAreaFound = true;
        SendMessageUpwards("OnFindClearArea");

	    if (audioSource.clip == null)
	    {
	        Debug.LogError("Could not find clear area sound clip");
	        return;
	    }

	    if (audioSource == null)
	    {
	        Debug.LogError("Could not find Player audio source while playing clear aread sound");
	        return;
	    }

	    if (!audioSource.isPlaying && clearAreaFound)
	    {
	        audioSource.clip = FoundAreaAudioClip;
            audioSource.Play();
        }	        	    
	}

    void OnTriggerStay(Collider collider)
    {       
        Debug.Log("Clear are Collided with " + collider.name);

        lastCollisionSeconds = 0;
    }
}
