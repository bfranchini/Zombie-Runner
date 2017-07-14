using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Zombie : MonoBehaviour
{
    public float Health = 100f;
    public float DamagePerHit = 20f;
    public float AttackRange = 1.5f;
    private Animator animator;    
    private bool isDead;
    private Transform enemyEyes;
	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
	    enemyEyes = GameObject.FindGameObjectWithTag("EnemyEyes").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (isDead) return;
        //TODO: figure out how to stop zombie movement while attacking.Look @ characterControl script

        if (Health > 0f)
	    {
	        RaycastHit hit;

            Debug.DrawRay(enemyEyes.position, enemyEyes.forward * AttackRange, Color.red);

	        var hitSomething = Physics.Raycast(enemyEyes.position, enemyEyes.forward, out hit, AttackRange);

	        if (!hitSomething) return;

	        var player = hit.collider.GetComponent<Player>();

	        if (player != null)
                //stop attacking player once he's dead
	            animator.SetBool("IsAttacking", !player.IsDead); 
	        else
                //stop attacking player if he's out of range
	            animator.SetBool("IsAttacking", false);

	        return;
        }
        
        isDead = true;
	    GetComponent<NavMeshAgent>().enabled = false;
	    GetComponent<AICharacterControl>().enabled = false;

        animator.SetTrigger("FallBack");
	}

    public void Damage(float damage)
    {
        Health -= damage;
    }
}
