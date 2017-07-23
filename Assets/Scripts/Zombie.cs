using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Zombie : MonoBehaviour
{
    public int DamagePerHit = 20;
    public float AttackRange = 1.5f;
    public AudioClip[] Talking;
    public AudioClip Death;
    private Health health;
    private Animator animator;
    private bool isDead;
    private bool isAttacking;
    private Transform enemyEyes;
    private AudioSource audioSource;
    private float nextSpeak; //holds time at which zombie will speak again    
    private int maxSpeakInterval = 5;
    private int minSpeakInterval = 3;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyEyes = GameObject.FindGameObjectWithTag("EnemyEyes").transform;
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        //TODO: figure out how to stop zombie movement while attacking.Look @ characterControl script

        if (health.GetCurrentHealth() > 0)
        {
            RaycastHit hit;

            Debug.DrawRay(enemyEyes.position, enemyEyes.forward * AttackRange, Color.red);

            var hitSomething = Physics.Raycast(enemyEyes.position, enemyEyes.forward, out hit, AttackRange);

            if (!hitSomething && Time.time > nextSpeak && !audioSource.isPlaying)
            {
                playClip(Talking[Random.Range(0, Talking.Length)]);       
                
                nextSpeak = Time.time + audioSource.clip.length + Random.Range(minSpeakInterval, maxSpeakInterval);
                return;
            }
                       
             if(hit.collider == null)
                return;            

            var player = hit.collider.GetComponent<Player>();

            if (player != null)            
                animator.SetBool("IsAttacking", !player.IsDead);                
            else
                animator.SetBool("IsAttacking", false);               

            return;
        }

        isDead = true;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AICharacterControl>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false; //prevent further collisions

        playClip(Death);

        animator.SetTrigger("FallBack");
    }

    private void playClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
