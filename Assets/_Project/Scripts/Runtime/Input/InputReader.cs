using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputMap;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Scriptables/Input Reader")]
public class InputReader : ScriptableObject, IGameplayActions
{
    public Vector2 Movement { get; private set; }

    public event Action OnButtonSouthUp;
    public event Action OnButtonSouthDown;

    public event Action OnPauseUp;
    public event Action OnPauseDown;

    public event Action OnButtonNorthUp;
    public event Action OnButtonNorthDown;

    public event Action OnButtonWestUp;
    public event Action OnButtonWestDown;

    public event Action OnRightTriggerUp;
    public event Action OnRightTriggerDown;

    private PlayerInputMap controls;

    public void EnableInput()
    {
        controls ??= new PlayerInputMap();

        controls.Gameplay.SetCallbacks(this);
        controls.Gameplay.Enable();
    }

    public void DisableInput()
    {
        controls.Gameplay.RemoveCallbacks(this);

        controls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
            OnPauseUp?.Invoke();
        else if (context.canceled)
            OnPauseDown?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            OnButtonSouthDown?.Invoke();
        else if (context.canceled)
            OnButtonSouthUp?.Invoke();
    }

    public void OnButtonNorth(InputAction.CallbackContext context)
    {
        if (context.started)
            OnButtonNorthDown?.Invoke();
        else if (context.canceled)
            OnButtonNorthUp?.Invoke();
    }

    public void OnButtonWest(InputAction.CallbackContext context)
    {
        if (context.started)
            OnButtonWestDown?.Invoke();
        else if (context.canceled)
            OnButtonWestUp?.Invoke();
    }

    public void OnRightTrigger(InputAction.CallbackContext context)
    {
        if (context.started)
            OnRightTriggerDown?.Invoke();
        else if (context.canceled)
            OnRightTriggerUp?.Invoke();
    }
}
