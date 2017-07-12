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
    public GameObject BulletEmitter;
    public GameObject BloodSquib;
    private Animator animator;
    private AudioSource audioSource;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f); //how long laser should be visible after gun is fired
    private LineRenderer laserLine;
    private float nextFire; //holds time at which player will be allowed to fire again
    private Camera camera;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        laserLine = GetComponent<LineRenderer>();
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

            //take position relative to the camera and converts it to point in world space
            //camera viewport has coordinates from (0,0) to (1,1). using .5f for x and y 
            //gets center of camera. 0 for z-axis give position exactly where player is
            var rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            var hit = new RaycastHit();

            //establish initial position of line
            laserLine.SetPosition(0, BulletEmitter.transform.position);

            //todo: remove after testing
            //Debug.DrawRay(rayOrigin, camera.transform.forward * WeaponRange, Color.green);

            if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, WeaponRange))
            {
                //we hit something
                laserLine.SetPosition(1, hit.point);
                
                if (hit.collider.tag == "EnemyHitBox"/* != null*/)
                {
                    var zombie = hit.collider.GetComponentInParent<Zombie>();
                    zombie.Damage(GunDamage);
                    Instantiate(BloodSquib, hit.point, Quaternion.identity, zombie.transform);
                }
                    
            }            
                
            else
                //we hit nothing. Draw laster line 50ft out from gun. forward specifies direction, weapon.range specifies distance
                laserLine.SetPosition(1, rayOrigin + (camera.transform.forward * WeaponRange));            
        }
    }


    private IEnumerator Fire()
    {
        //Instantiate(Bullet, BulletEmitter.transform.position, BulletEmitter.transform.rotation);

        audioSource.clip = GunFire;
        audioSource.Play();
        animator.SetTrigger("Fire");
        //todo: Re-enable
        //   BulletCount--;

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
