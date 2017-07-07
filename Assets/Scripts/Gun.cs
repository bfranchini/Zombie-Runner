using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int BulletCount = 6;
    public AudioClip GunFire;
    private Animator animator;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();

	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Fire()
    {
        if (audioSource.isPlaying && animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
            return;

        audioSource.clip = GunFire;        
        audioSource.Play();
        animator.SetTrigger("Fire");
    }
}
