using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public string tagObj = "Enemy";

    public float damage = 1f;

    public float knocbackF = 20f;

    public float moveSpeed = 50;

    public DetectionZone detectionZone;

    public string tagEnemy = "Enemy";

    private float waitTime;

    public float startTimeWait;

    public Transform[] moveSpot;

    private int randomSpot;

    Rigidbody2D rb;

    Animator animator;

    SpriteRenderer spriteRenderer;

    DamagebleCharacter damagebleCharacter;

    void Start()
    {
        waitTime = startTimeWait;
        randomSpot = Random.Range(0, moveSpot.Length);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        damagebleCharacter = GetComponent<DamagebleCharacter>();
    }
    void Update()
    {
        if (moveSpot.Length == 0 && damagebleCharacter.Targetable && detectionZone.detectedObjs.Count == 0)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
        }
        if (damagebleCharacter.Targetable && detectionZone.detectedObjs.Count == 0 && moveSpot.Length != 0)
        {
            animator.SetBool("IsMoving", true); 
            transform.position = Vector2.MoveTowards(transform.position, moveSpot[randomSpot].position, 0.6f * Time.deltaTime);
            Vector2 direction = (moveSpot[randomSpot].position - transform.position).normalized;

            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            if (Vector2.Distance(transform.position, moveSpot[randomSpot].position) < 0.2) 
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpot.Length);
                    waitTime += startTimeWait;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                    animator.SetBool("IsMoving", false);
                }
            }
        }

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
    }
    void OnCollisionStay2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Vector2 dir = (collider.gameObject.transform.position - transform.position).normalized;
            Vector2 knockback = dir * knocbackF;
            damageable.OnHit(damage, knockback, tagEnemy);
        }
        else
        {
            Debug.LogWarning("Error k");
        }
    }

}
