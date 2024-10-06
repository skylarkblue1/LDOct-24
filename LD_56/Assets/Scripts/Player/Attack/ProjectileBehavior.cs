using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ProjectileBehavior : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField]
    private float initialForce;
    [SerializeField]
    [Min(0f)]
    private float damageValue;
    [SerializeField]
    [Tooltip("How long this projectile is active for if it never hits a collider")]
    [Min(0.1f)]
    private float lifetime;

    private float timer;

    Rigidbody rb;
    Collider projectileCollider;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        projectileCollider = GetComponent<Collider>();
    }

    private void OnEnable() {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * initialForce, ForceMode.Impulse);
        timer = lifetime;
    }

    private void FixedUpdate() {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0) {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy")) {
            // Damage Enemy
            if (other.gameObject.TryGetComponent(out DamageEnemy damageEnemy))
            {
                damageEnemy.Damage(damageValue);
            }
        }
        gameObject.SetActive(false);
    }
}
