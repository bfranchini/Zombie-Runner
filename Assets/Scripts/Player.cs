using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public GameObject LandingAreaPrefab;
    public bool IsDead;
    public bool AllowSpawning = true;
    public bool HasPhone;
    private Health health;
    private Transform[] spawnPoints;
    private bool Respawn; //used for testing respawn points(make public and toggle)   
    private UI ui;    
    private bool helicopterCalled;
    private ClearAreaDetector clearAreaDetector;
    private FirstPersonController firstPersonController;
    private Gun gun; 

    // Use this for initialization
    void Start()
    {
        var spawnParent = GameObject.Find("Player Spawn Points");
        clearAreaDetector = FindObjectOfType<ClearAreaDetector>();
        firstPersonController = GetComponent<FirstPersonController>();
        gun = GetComponentInChildren<Gun>();
        health = GetComponent<Health>();

        if (spawnParent == null)
        {
            Debug.Log("Could not find spawn point parent");
            return;
        }

        spawnPoints = spawnParent.GetComponentsInChildren<Transform>();

        if (AllowSpawning)
            spawn();

        ui = FindObjectOfType<UI>();
        ui.UpdateHealth(health.GetCurrentHealth());
        ui.SetNotificationText("Find a phone so you can call the helicopter.");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
            return;

        if (health.GetCurrentHealth() <= 0)
        {
            IsDead = true;
            KillPlayer();
        }

        if (Respawn)
            spawn();

        if (Input.GetButtonDown("CallHeli") && HasPhone && !helicopterCalled)
        {
            if (clearAreaDetector.CallHelicopter())
                helicopterCalled = true;
        }
    }

    private void spawn()
    {
        Respawn = false;

        if (spawnPoints.Length == 0)
            return;

        var respawnPoint = spawnPoints[Random.Range(1, spawnPoints.Length)];

        transform.position = respawnPoint.position;
    }

    private void OnFindClearArea()
    {
        if (IsDead) return;

        Debug.Log("Found clear area");
        Invoke("DropFlare", 3f);
    }

    private void DropFlare()
    {
        var flarePosition = new Vector3(transform.position.x, LandingAreaPrefab.transform.position.y, transform.position.z);
        Instantiate(LandingAreaPrefab, flarePosition, transform.rotation);
    }

    private void KillPlayer()
    {
       firstPersonController.enabled = false;
        var cameraAnimator = GetComponent<Animator>();
        cameraAnimator.SetTrigger("PlayerDied");
        ui.GetComponent<Animator>().SetTrigger("PlayerDied");
    }

    public void Damage(int damage)
    {
        ui.UpdateHealth(health.TakeDamage(damage));
    }

    public void SetPhone(bool value)
    {
        HasPhone = value;
    }

    public void EndGame()
    {
        firstPersonController.enabled = false;
        gun.enabled = false;
        GetComponentInChildren<Eyes>().enabled = false;
    }
}
