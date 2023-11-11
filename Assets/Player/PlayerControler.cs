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

    public DamagebleCharacter character;

    public float maxSpeed = 8f;

    public GameObject swordHitbox;

    public float idleFriction = 0.9f;

    Vector2 movemenInput = Vector2.zero;

    SpriteRenderer spriteRenderer;

    private bool active;

    public Rigidbody2D rb;

    Animator animator;

    public GameObject b;

    Collider2D swordCollider;

    bool canMove = true;
    void Start()
    {
        b.SetActive(false);
        active = false;
        transform.position = pos.inilealValue;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            active = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            active = false;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == tagTarget)
        {
            b.SetActive(true);
            if (active)
            {
                damage += col.gameObject.GetComponent<BaseItemstats>().damage;
                moveSpeed += col.gameObject.GetComponent<BaseItemstats>().speed;
                character._maxhealth += col.gameObject.GetComponent<BaseItemstats>().health;
                Destroy(col.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == tagTarget)
        {
            b.SetActive(false);
        }
    }
    private void FixedUpdate()  
    {
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
