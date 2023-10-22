using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IDamageable
{
    Animator animator;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("Hit");
            }

            _health = value;

            if ( _health <= 0 ) 
            {
                animator.SetBool("Defeated", true);
            }
        }
        get 
        {
            return _health; 
        }
    }

    public float _health = 3;

    void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}
