using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class NewPlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;
    [SerializeField] private InputActionReference movement, attack, pointerPosition;
    private void Update()
    {
        OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>());
        OnPointerInput?.Invoke(GetPointerInput());
    }
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = UnityEngine.Camera.main.nearClipPlane;
        return UnityEngine.Camera.main.ScreenToWorldPoint(mousePos);
    }
    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }
    private void PerformAttack(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }
}