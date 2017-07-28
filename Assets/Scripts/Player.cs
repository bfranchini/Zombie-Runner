using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public GameObject LandingAreaPrefab;        
    public bool IsDead;
    private Health health;
    private Transform[] spawnPoints;
    private bool Respawn; //used for testing respawn points(make public and toggle)   
    private UI ui;

    // Use this for initialization
    void Start()
    {
        var spawnParent = GameObject.Find("Player Spawn Points");
        health = GetComponent<Health>();

        if (spawnParent == null)
        {
            Debug.Log("Could not find spawn point parent");
            return;
        }

        spawnPoints = spawnParent.GetComponentsInChildren<Transform>();
        ui = FindObjectOfType<UI>();
        ui.UpdateHealth(health.GetCurrentHealth());
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
            ReSpawn();                   
    }

    private void ReSpawn()
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

    public void Damage(int damage)
    {
        ui.UpdateHealth(health.TakeDamage(damage));
    }

    private void KillPlayer()
    {
        GetComponent<FirstPersonController>().enabled = false;
        var cameraAnimator = GetComponent<Animator>();
        cameraAnimator.SetTrigger("PlayerDied");
        ui.GetComponent<Animator>().SetTrigger("PlayerDied");
    }
}
