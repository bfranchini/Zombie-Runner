using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int MagazineBulletCount;
    public int TotalAmmo = 30;
    public int GunDamage = 34;
    public float FireRate = .25f;
    public float WeaponRange = 50f;
    public AudioClip GunFire;
    public GameObject BloodSquib;
    private Animator animator;
    private int magSize = 6;
    private AudioSource audioSource;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f); //how long laser should be visible after gun is fired   
    private float nextFire; //holds time at which player will be allowed to fire again
    private Camera camera;
    private Player player;
    private UI ui;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        camera = GetComponentInParent<Camera>();
        player = FindObjectOfType<Player>();
        MagazineBulletCount = magSize;
        ui = FindObjectOfType<UI>();
        ui.UpdateMagCount(MagazineBulletCount);
        ui.UpdateAmmoCount(TotalAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead) return;

        if (MagazineBulletCount <= 0)
        {
            Reload();
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && MagazineBulletCount > 0)
        {
            if (MagazineBulletCount <= 0)
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
        MagazineBulletCount--;
        ui.UpdateMagCount(MagazineBulletCount);

        yield return shotDuration;
    }

    public void Reload()
    {
        if (TotalAmmo <= 0)
            return;

        if (TotalAmmo >= magSize)
        {
            TotalAmmo -= magSize;
            MagazineBulletCount = magSize;
        }
        else
        {
            MagazineBulletCount = TotalAmmo;
            TotalAmmo = 0;
        }

        ui.UpdateMagCount(MagazineBulletCount);
        ui.UpdateAmmoCount(TotalAmmo);
    }
}
