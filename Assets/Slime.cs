using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1f;

    public float knocbackF = 20f;
    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            if (damageable != null)
            {
                Vector2 dir = (collider.gameObject.transform.position - transform.position).normalized;
                Vector2 knockback = dir * knocbackF;
                print(knockback);

                damageable.OnHit(damage, knockback);
            }
        }
    }

}
