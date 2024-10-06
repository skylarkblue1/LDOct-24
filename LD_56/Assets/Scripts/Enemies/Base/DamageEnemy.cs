using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class DamageEnemy : MonoBehaviour
{
    private EnemyStats enemyStats;
    private float health;
    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        health = enemyStats.Health;
    }
    public void Damage(float amount)
    {
        if (health > 0)
        {
            health -= amount;
            return;
        }
        Destroy(gameObject);
    }
}
