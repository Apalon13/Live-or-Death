using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagebleCharacter : MonoBehaviour, IDamageable
{
    public bool disableSimulation = false;

    public float invincibilitiTime = 0.25f;

    public bool isinvincibilitiEnable = false;

    private float invincibleTimeElapsed = 0f;

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

    public bool Invincible
    {
        get
        {
            return _invincible;
        }
        set
        {
            _invincible = value;

            if(Invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
            Debug.Log(Invincible);
        }

    }
    public float _health = 3;

    bool _targetable = true;

    bool _invincible = false;

    void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage)
    {
        if (!isinvincibilitiEnable || !Invincible)
        {
            Health -= damage;
        }

        if (isinvincibilitiEnable)
        {
            Invincible = true;
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if(!isinvincibilitiEnable! || !Invincible)
        {
            AudioManager.instance.Play("Damage");
            Health -= damage;
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }

        if(isinvincibilitiEnable)
        {
            Invincible = true;
        }
    }
    public void OnObjectDestroy()
    {
        GameObject.Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;

            if(invincibleTimeElapsed > invincibilitiTime)
            {
                Invincible = false;
            }
        }
    }
}
