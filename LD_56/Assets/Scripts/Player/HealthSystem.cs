using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
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
}
