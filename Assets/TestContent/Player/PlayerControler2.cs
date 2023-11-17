using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler2 : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputActionReference pointerPosition;
    private Vector2 movemenInput;
    private bool canMove = true;
    public float maxSpeed;
    public float moveSpeed;
    public float idleFriction;
    void Update()
    {

        if (canMove == true && movemenInput != Vector2.zero)
        {
            //rb.AddForce(movement * moveSpeed * Time.deltaTime);
            //rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movement * moveSpeed * Time.deltaTime), maxSpeed);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movemenInput * moveSpeed * Time.deltaTime), maxSpeed);

            if (rb.velocity.magnitude > maxSpeed)
            {
                float limited = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limited;
            }
        }
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        AnimUpdate(movement);
        if (animator.GetFloat("Horizontal") > 0)
        {
            animator.SetFloat("IdleVector", 1);
        }
        if (animator.GetFloat("Horizontal") < 0)
        {
            animator.SetFloat("IdleVector", -1);
        }
    }
    private void AnimUpdate(Vector2 movement)
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }
    private void OnMove(InputValue value)
    {
        movemenInput = value.Get<Vector2>();
    }
    private void OnFire()
    {
        animator.SetTrigger("Attack");
    }
    void LockMovement()
    {
        canMove = false;
    }
    void UnLockMovement()
    {
        canMove = true;
    }
    private void MoveCrossHair()
    {

    }
}
