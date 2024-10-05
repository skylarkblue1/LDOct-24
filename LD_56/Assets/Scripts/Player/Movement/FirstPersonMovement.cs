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
    float forwardSpeed;
    [SerializeField]
    float backSpeed;
    [SerializeField]
    float strafeSpeed;

    PlayerControls playerControls;
    CharacterController controller;

    private void Awake() {
        controller = GetComponent<CharacterController>();

        playerControls = new();
    }

    private void OnEnable() {
        playerControls.Movement.Enable();
    }

    private void OnDisable() {
        playerControls.Movement.Disable();
    }

    private void Start() {

    }

    void Update()
    {
        Move();
    }

    private void Move() {
        // if (playerControls.Movement.Forward.IsPressed()) {
        //     Vector3 forward = transform.TransformDirection(Vector3.forward);
        //     controller.SimpleMove(forward * forwardSpeed);
        // }

        // if (playerControls.Movement.Backward.IsPressed()) {
        //     Vector3 back = transform.TransformDirection(Vector3.back);
        //     controller.SimpleMove(back * backSpeed);
        // }

        // if (playerControls.Movement.StrafeLeft.IsPressed()) {
        //     Vector3 left = transform.TransformDirection(Vector3.left);
        //     controller.SimpleMove(left * strafeSpeed);
        // }

        // if (playerControls.Movement.StrafeRight.IsPressed()) {
        //     Vector3 right = transform.TransformDirection(Vector3.right);
        //     controller.SimpleMove(right * strafeSpeed);
        // }

        if (playerControls.Movement.Forward.IsPressed()) {
            // Vector3 forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(transform.forward * forwardSpeed);
        }

        if (playerControls.Movement.Backward.IsPressed()) {
            // Vector3 back = transform.TransformDirection(Vector3.back);
            controller.SimpleMove(-transform.forward * backSpeed);
        }

        if (playerControls.Movement.StrafeLeft.IsPressed()) {
            // Vector3 left = transform.TransformDirection(Vector3.left);
            controller.SimpleMove(-transform.right * strafeSpeed);
        }

        if (playerControls.Movement.StrafeRight.IsPressed()) {
            // Vector3 right = transform.TransformDirection(Vector3.right);
            controller.SimpleMove(transform.right * strafeSpeed);
        }
    }
}
