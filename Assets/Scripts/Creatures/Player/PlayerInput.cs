using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private PlayerInputAction action;

    private void Awake()
    {
        player = GetComponent<Player>();
        action = new PlayerInputAction();

        action.Player.Movement.performed += OnMovement;
        action.Player.Movement.canceled += OnMovement;

        action.Player.Shoot.performed += OnShoot;
        action.Player.Kick.performed += OnKick;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        player.Movement(context.ReadValue<Vector2>());
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        player.Shoot();
    }

    private void OnKick(InputAction.CallbackContext context)
    {
        player.TryKick();
    }

    private void OnEnable()
    {
        action.Enable();
    }
}
