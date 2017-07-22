using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public GameObject LandingAreaPrefab;    
    public float PlayerHealth = 100f;
    public bool IsDead;    
    private Transform[] spawnPoints;
    private bool Respawn; //used for testing respawn points(make public and toggle)   
    private UI ui;

    // Use this for initialization
    void Start()
    {
        var spawnParent = GameObject.Find("Player Spawn Points");

        if (spawnParent == null)
        {
            Debug.Log("Could not find spawn point parent");
            return;
        }

        spawnPoints = spawnParent.GetComponentsInChildren<Transform>();
        ui = FindObjectOfType<UI>();
        ui.UpdateHealth(PlayerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Respawn)
            ReSpawn();

        if (IsDead)
            Debug.Log("Player has died");
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

    void OnTriggerEnter(Collider collider)
    {
        if (IsDead)
            return;

        if (collider.transform.tag != "ZombieHand") return;

        var damage = collider.transform.GetComponentInParent<Zombie>().DamagePerHit;
            
        PlayerHealth -= damage;

        ui.UpdateHealth(PlayerHealth);

        if (PlayerHealth <= 0)
        {
            IsDead = true;

            GetComponent<FirstPersonController>().enabled = false;
        }            
    }
}
