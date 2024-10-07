using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField]
    private int health;

    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        maxHealth = health;
    }

    public void Update()
    {
        if (Input.GetKey("p"))
        {
            // just for debugging
            health = health - 10;
            Debug.Log(health);
        }

        if (health <= 0)
        {
            Debug.Log("ded");
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void IncreaseHealth(int amount)
    {
        this.health = Mathf.Min(maxHealth, health + amount);
    }

    public void DecreaseHealth(int amount)
    {
        this.health = Mathf.Max(0, health - amount);
        Debug.Log(health);
    }

    public void SetHealth(int health)
    {
        this.health = Mathf.Min(maxHealth, health);
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return health;
    }
}
