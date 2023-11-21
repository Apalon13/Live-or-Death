using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterSprite, weaponSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private Stats stats;
    public Vector2 PointerPosition {  get; set; }
    public float delay = 0.5f;
    public float damage;
    private bool attackBloked;
    public bool IsAttacking {  get; private set; }
    public Transform circlOrygin;
    public float radius;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Update()
    {
        if (stats != null)
        {
            damage = stats.damage;
        }
        if(IsAttacking)
        {
            return;
        }
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }else if (direction.x > 0) 
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponSprite.sortingOrder = characterSprite.sortingOrder - 1;
        }
        else
        {
            weaponSprite.sortingOrder = characterSprite.sortingOrder + 1;
        }
        
    }

    public void Attack()
    {
        if (attackBloked)
        {
            return;
        }
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBloked = true;
        StartCoroutine(DelayAttack());
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBloked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circlOrygin == null ? Vector3.zero : circlOrygin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circlOrygin.position, radius))
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
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
    }
}