using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set;}

    private PlayerControls playerControls;

     private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 

        playerControls = new();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable() {
        playerControls.Movement.Enable();
        playerControls.Interact.Enable();
    }

    private void OnDisable() {
        playerControls.Movement.Disable();
        playerControls.Interact.Disable();
    }

    public Vector2 GetPlayerMovement() {
        return playerControls.Movement.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta() {
        return playerControls.Movement.Look.ReadValue<Vector2>();
    }

    public bool AttackKeyPressed() {
        return playerControls.Interact.Attack.IsPressed();
    }
}
