using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    public static InputManager Instance;

    public event EventHandler OnJumpAction;

    private InputSystem_Actions inputSystemActions;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one InputManager Instance");
        }
        Instance = this;

        inputSystemActions = new InputSystem_Actions();

        inputSystemActions.Player.Enable();

        inputSystemActions.Player.Jump.performed += Jump_performed;
    }

    private void OnDestroy() {
        inputSystemActions.Player.Jump.performed -= Jump_performed;

        inputSystemActions.Dispose();
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }
}

