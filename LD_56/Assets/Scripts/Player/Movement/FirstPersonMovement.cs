using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    public float moveSpeed;
    [SerializeField]
    float gravityValue;

    [Header("Look Controls")]

    CharacterController controller;
    private Vector3 playerVelocity;
    private InputManager inputManager;
    private Transform cameraTransform;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Start() {

    }

    void Update()
    {
        Move();
        Gravity();
    }
    
    private void Move() {
        Vector2 movement = InputManager.Instance.GetPlayerMovement();
        Vector3 move = new (movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(moveSpeed * Time.deltaTime * move);
        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
        }
    }

    private void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
