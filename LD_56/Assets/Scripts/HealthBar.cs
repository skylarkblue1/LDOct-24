using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public Image imgHealthBar;

    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = player.GetComponent<HealthSystem>();
    }

    private float previousFrameHealth;

    private void Update()
    {
        float maxHealth = healthSystem.GetMaxHealth();
        float health = healthSystem.GetHealth();

        if (previousFrameHealth != health)
        {
            float barScaled = (health / maxHealth);
            imgHealthBar.fillAmount = barScaled;
            previousFrameHealth = health;
        }
    }
}
