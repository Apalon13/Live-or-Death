using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public static string tagObj = "Player";

    public VectorValue pos;

    public float damage = 10;

    public string tagTarget = "Item";

    public float moveSpeed = 30f;

    public float maxSpeed = 8f;

    public GameObject swordHitbox;

    public float idleFriction = 0.9f;

    Vector2 movemenInput = Vector2.zero;

    SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;

    Animator animator;

    Collider2D swordCollider;

    bool canMove = true;
    void Start()
    {
        transform.position = pos.inilealValue;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == tagTarget)
        {
            damage += col.gameObject.GetComponent<BaseItemstats>().damage;
            Destroy(col.gameObject);
        }
    }
    private void FixedUpdate()  
    {
        print(transform.position);
        if (canMove == true && movemenInput != Vector2.zero)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movemenInput * moveSpeed * Time.deltaTime), maxSpeed);
            //rb.AddForce(movemenInput * moveSpeed * Time.deltaTime);

            if (rb.velocity.magnitude > maxSpeed)
            {
                float limited = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limited; 
            }

            animator.SetBool("IsMoving", true);

            if (movemenInput.x < 0)
            {
                gameObject.BroadcastMessage("IsFacingRight", true);
                spriteRenderer.flipX = true;
            }
            else if (movemenInput.x > 0)
            {
                gameObject.BroadcastMessage("IsFacingRight", false);
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }

    void OnMove(InputValue value)
    {
        movemenInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }
    void LockMovement()
    {
        canMove = false;
    }
    void UnLockMovement()
    {
        canMove = true;
    }
}
