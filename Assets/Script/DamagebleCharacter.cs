using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagebleCharacter : MonoBehaviour, IDamageable
{
    public bool disableSimulation = false;

    Animator animator;

    Rigidbody2D rb;

    Collider2D physicsCollider;

    private void Start()
    {
        physicsCollider = GetComponent<Collider2D>();
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

            if (_health <= 0)
            {
                animator.SetBool("Defeated", true);
                Targetable = false;
            }
        }
        get
        {
            return _health;
        }
    }

    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;

            if (disableSimulation)
            {
                rb.simulated = false;
            }
            

            physicsCollider.enabled = value;
        }

    }

    public float _health = 3;

    public bool _targetable = true;

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
    public void OnObjectDestroy()
    {
        GameObject.Destroy(gameObject);
    }

}
