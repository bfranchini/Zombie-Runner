using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Zombie : MonoBehaviour
{
    public float Health = 100f;
    public float damage = 20f;
    private Animator animator;    
    private bool isDead;

	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (isDead) return;

	    if (Health > 0f) return;

	    isDead = true;
	    GetComponent<NavMeshAgent>().enabled = false;
	    GetComponent<AICharacterControl>().enabled = false;

        animator.SetTrigger("FallBack");
	}

    void OnTriggerEnter(Collider collider)
    {
        var player = collider.transform.GetComponent<Player>();

        if (player == null) return;

        animator.SetBool("IsAttacking", !player.IsDead);

        //TODO: figure out how to stop zombie movement while attacking. Look @ characterControl script
        //          aiCharacterControl.move = false;
        //aiCharacterControl.target = null;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.transform.GetComponent<Player>() != null)
        {
            animator.SetBool("IsAttacking", false);
//            aiCharacterControl.move = true;
            //aiCharacterControl.target = target;
        }            
    }
}
