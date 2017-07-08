using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int BulletCount = 6;
    public AudioClip GunFire;
    public GameObject Bullet;
    public GameObject BulletEmitter;
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
        if (BulletCount > 0)
        {
            if (audioSource.isPlaying && animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
                return;

            Instantiate(Bullet, BulletEmitter.transform.position, BulletEmitter.transform.rotation);

            audioSource.clip = GunFire;
            audioSource.Play();
            animator.SetTrigger("Fire");
            //todo: Re-enable
            //   BulletCount--;
        }
        else
        {
            Debug.Log("Reload!");
        }
    }
}
