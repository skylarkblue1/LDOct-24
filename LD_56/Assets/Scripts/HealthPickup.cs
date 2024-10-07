using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public HealthSystem health;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player walked into the health pickup");
            health.health = Mathf.Min(health.health + 10, health.maxHealth);
            Debug.Log(health.health);
            Destroy(gameObject);
        }
    }
}
