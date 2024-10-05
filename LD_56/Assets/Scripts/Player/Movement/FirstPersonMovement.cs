using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField]
    float moveSpeed;

    [Header("Look Controls")]

    PlayerControls playerControls;
    CharacterController controller;

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void Start() {

    }

    void Update()
    {
        Move();
    }

    private void Move() {
        Vector2 movement = InputManager.Instance.GetPlayerMovement();
        Vector3 move = new (movement.x, 0f, movement.y);
        controller.Move(moveSpeed * Time.deltaTime * move);
        if (move != Vector3.zero) {
            gameObject.transform.forward = movement;
        }
    }
}
