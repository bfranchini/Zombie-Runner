using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public GameObject LandingAreaPrefab;
    public bool IsDead;
    public bool AllowSpawning = true;
    private Health health;
    private Transform[] spawnPoints;
    private bool Respawn; //used for testing respawn points(make public and toggle)   
    private UI ui;
    private bool hasPhone;
    private bool helicopterCalled;
    private ClearAreaDetector clearAreaDetector;

    // Use this for initialization
    void Start()
    {
        var spawnParent = GameObject.Find("Player Spawn Points");
        clearAreaDetector = FindObjectOfType<ClearAreaDetector>();
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

        if (Input.GetButtonDown("CallHeli") && hasPhone && !helicopterCalled)
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
        GetComponent<FirstPersonController>().enabled = false;
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
        hasPhone = value;
    }
}
