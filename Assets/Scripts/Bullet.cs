using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 762f;
    public float BulletLife = 2f;

	// Use this for initialization
	void Start ()
	{
		Destroy(gameObject, BulletLife);
    }
	
	// Update is called once per frame
	void Update () {        
		transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Bullet collided with " + collider.name);

        //todo: clean this up somehow?
        if (collider.name != "Landing Area" && collider.name != "Clear Area" && collider.name != "Player")        
         Destroy(gameObject);
    }
}
