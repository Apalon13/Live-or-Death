using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1f;
    [SerializeField] private float maxSpeed = 2, acceleration = 50, deacceleration = 100;
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }
    public DetectionZone detectionZone;
    private float waitTime;
    public float startTimeWait;
    public Transform[] moveSpot;
    private int randomSpot;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    HP Hp;

    void Start()
    {
        waitTime = startTimeWait;
        randomSpot = Random.Range(0, moveSpot.Length);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Hp = GetComponent<HP>();
    }
    void Update()
    {
        if (moveSpot.Length == 0 && Hp.Targetable && detectionZone.detectedObjs.Count == 0)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
        }
        if (Hp.Targetable && detectionZone.detectedObjs.Count == 0 && moveSpot.Length != 0)
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

        if (Hp.Targetable && detectionZone.detectedObjs.Count > 0)
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
            if(Hp.Targetable && detectionZone.detectedObjs.Count > 0)
            {
                oldMovementInput = direction;
                currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
            }
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            rb.velocity = oldMovementInput * currentSpeed;
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.OnHit(damage, gameObject);
        }
        else
        {
            Debug.LogWarning("Error k");
        }
    }



}
