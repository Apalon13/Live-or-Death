using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private NewAgentAnimations agentAnimations;
    public VectorValue pos;
    private AgentMover agentMover;
    private NewWeaponParent weaponParent;
    private Vector2 pointerInput, movementInput;
    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Update()
    {
        agentMover.MovementInput = MovementInput;
        weaponParent.PointerPosition = pointerInput;
        AnimateCharacter();
    }

    public void PerformAttack()
    {
        weaponParent.Attack();
    }

    private void Awake()
    {
        transform.position = pos.inilealValue;
        agentAnimations = GetComponentInChildren<NewAgentAnimations>();
        weaponParent = GetComponentInChildren<NewWeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.PlayAnimation(MovementInput);
        agentAnimations.AnimatePosition(lookDirection);
    }

    

}