using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private RangedWeapon currentWeapon;

    // Leave this for enemy ai to read.
    public int attackDamage;

    private InputManager inputManager;
    private void Awake() {
        inputManager = InputManager.Instance;
    }

    private void Update() {
        if (inputManager.AttackKeyPressed()) ProcessAttack();
    }

    private void ProcessAttack() {
        if (currentWeapon == null) return;
        currentWeapon.Fire();
    }
}
