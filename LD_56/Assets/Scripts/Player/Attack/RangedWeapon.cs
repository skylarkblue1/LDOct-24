using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField]
    [Tooltip("Time between firing projectiles")]
    private float cooldown;

    [SerializeField]
    [Tooltip("Projectile spawn point")]
    private Transform spawnPos;

    [SerializeField]
    private RandomizedObjectPool ammoPool;

    private float timer;

    private void Update() {
        timer += Time.deltaTime;
        Mathf.Clamp(timer, 0f, cooldown + 1f); // To prevent the timer from going into infinity
    }

    public void Fire() {
        if (timer < cooldown) { return; }
        timer = 0f;
        GameObject curProjectile = ammoPool.GetRandomObject();
        curProjectile.transform.forward = Camera.main.transform.forward;
        curProjectile.transform.position = spawnPos.position;
        curProjectile.layer = 7;
        curProjectile.SetActive(true);
    }    
}
