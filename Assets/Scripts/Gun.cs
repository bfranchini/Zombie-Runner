using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{    
    public int CurrentAmmo = 30; //total amount of ammo player currently has
    public int MaxAmmo = 60; //max ammount of ammo player can carry at a given time
    public int GunDamage = 34;//it takes three shots to kill a zombie
    public float FireRate = .25f;
    public float WeaponRange = 50f;
    public AudioClip GunFire;
    public AudioClip ReloadClip;
    public AudioClip EmptyMag;
    public GameObject BloodSquib;
    public int MagSize = 6; //max number of bullets in per magazine
    private Animator animator;
    private int magazineBulletCount; //current number of bullets in magazine    
    private AudioSource audioSource;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f); //how long laser should be visible after gun is fired   
    private float nextFire; //holds time at which player will be allowed to fire again
    private Camera playerCamera;
    private Player player;
    private UI ui;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerCamera = GetComponentInParent<Camera>();
        player = FindObjectOfType<Player>();
        magazineBulletCount = MagSize;
        ui = FindObjectOfType<UI>();
        ui.UpdateMagCount(magazineBulletCount);
        ui.UpdateAmmoCount(CurrentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead) return;

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && CurrentAmmo <= 0 && magazineBulletCount <= 0)
        {
            audioSource.clip = EmptyMag;
            audioSource.Play();
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && !audioSource.isPlaying)
        {
            if (magazineBulletCount <= 0)
                Reload();
            
            //we check audiosource again because reload sounds could be playing
            if (audioSource.isPlaying || animator.GetCurrentAnimatorStateInfo(0).IsName("Fire") || animator.GetCurrentAnimatorStateInfo(0).IsName("ZoomedFire"))
                return;

            nextFire = Time.time + FireRate;

            StartCoroutine(Fire());

            //takes position relative to the camera and converts it to point in world space
            //camera viewport has coordinates from (0,0) to (1,1). Using .5f for x and y 
            //gets center of camera. 0 for z-axis give position exactly where player is
            //bullets are fired from center of camera
            var rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;

            //Debug.DrawRay(rayOrigin, camera.transform.forward * WeaponRange, Color.green);
            var hitSomething = Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, WeaponRange);

            if(hit.collider == null)
                return;

            var enemyHealth = hit.collider.GetComponent<Health>();

            if (!hitSomething || enemyHealth == null) return;
            
            enemyHealth.TakeDamage(GunDamage);

            var blood = Instantiate(BloodSquib, hit.point, Quaternion.identity, enemyHealth.transform);
            Destroy(blood, .25f);
        }

        if (Input.GetButtonDown("Reload") && CurrentAmmo > 0 && magazineBulletCount < MagSize)
        {
            //wait for any gun sounds to stop playing before reloading
            if(audioSource.isPlaying)
                Invoke("Reload", audioSource.clip.length - audioSource.time);
            else         
                Reload();            
        }
    }

    private IEnumerator Fire()
    {
        audioSource.clip = GunFire;
        audioSource.Play();
        animator.SetTrigger("Fire");
        magazineBulletCount--;
        ui.UpdateMagCount(magazineBulletCount);

        yield return shotDuration;
    }

    public void Reload()
    {
        if (CurrentAmmo <= 0)
            return;

        audioSource.clip = ReloadClip;
        audioSource.Play();

        if (CurrentAmmo >= MagSize)
        {
            CurrentAmmo -= MagSize;
            magazineBulletCount = MagSize;
        }
        else
        {
            magazineBulletCount = CurrentAmmo;
            CurrentAmmo = 0;
        }

        ui.UpdateMagCount(magazineBulletCount);
        ui.UpdateAmmoCount(CurrentAmmo);
    }

    public bool AddAmmo(int ammoCount)
    {
        if (CurrentAmmo >= MaxAmmo) return false;

        if (CurrentAmmo + ammoCount > MaxAmmo)
            CurrentAmmo += MaxAmmo - CurrentAmmo;
        else
            CurrentAmmo += ammoCount;

        ui.UpdateAmmoCount(CurrentAmmo);            
        return true;
    }
}
