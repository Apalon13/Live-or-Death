using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1f;

    public float knocbackF = 20f;

    public float moveSpeed = 50;

    public DetectionZone detectionZone;

    Rigidbody2D rb;

    Animator animator;

    SpriteRenderer spriteRenderer;

    DamagebleCharacter damagebleCharacter;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        damagebleCharacter = GetComponent<DamagebleCharacter>();
    }
    void FixedUpdate()
    {
        if (damagebleCharacter.Targetable && detectionZone.detectedObjs.Count > 0)
        {
            animator.SetBool("IsMoving", true);
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Vector2 dir = (collider.gameObject.transform.position - transform.position).normalized;
            Vector2 knockback = dir * knocbackF;

            damageable.OnHit(damage, knockback);
        }
        else
        {
            Debug.LogWarning("Error k");
        }
    }

}
