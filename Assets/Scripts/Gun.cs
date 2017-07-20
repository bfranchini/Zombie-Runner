using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int BulletCount = 6;
    public int GunDamage = 34;
    public float FireRate = .25f;
    public float WeaponRange = 50f;
    public AudioClip GunFire;
    public GameObject BloodSquib;
    private Animator animator;
    private AudioSource audioSource;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f); //how long laser should be visible after gun is fired   
    private float nextFire; //holds time at which player will be allowed to fire again
    private Camera camera;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        camera = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && BulletCount > 0)
        {
            if (BulletCount <= 0)
            {
                Debug.Log("Reload!");
                return;
            }

            if (audioSource.isPlaying && animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
                return;

            nextFire = Time.time + FireRate;

            StartCoroutine(Fire());

            //takes position relative to the camera and converts it to point in world space
            //camera viewport has coordinates from (0,0) to (1,1). Using .5f for x and y 
            //gets center of camera. 0 for z-axis give position exactly where player is
            //bullets are fired from center of camera
            var rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            
            //Debug.DrawRay(rayOrigin, camera.transform.forward * WeaponRange, Color.green);
            var hitSomething = Physics.Raycast(rayOrigin, camera.transform.forward, out hit, WeaponRange);

            if (!hitSomething || hit.collider.GetComponent<Zombie>() == null) return;

            var zombie = hit.collider.GetComponentInParent<Zombie>();
            zombie.Damage(GunDamage);

            var blood = Instantiate(BloodSquib, hit.point, Quaternion.identity, zombie.transform);
            Destroy(blood, .25f);
        }
    }

    private IEnumerator Fire()
    {        
        audioSource.clip = GunFire;
        audioSource.Play();
        animator.SetTrigger("Fire");
        //todo: Re-enable
        //   BulletCount--;

        yield return shotDuration;
    }
}
