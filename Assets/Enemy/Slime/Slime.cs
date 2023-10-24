using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1f;

    public float knocbackF = 20f;

    public float moveSpeed = 50;

    public DetectionZone detectionZone;

    Rigidbody2D rb;

    DamagebleCharacter damagebleCharacter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damagebleCharacter = GetComponent<DamagebleCharacter>();
    }
    void FixedUpdate()
    {
        if (damagebleCharacter.Targetable && detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }
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

                damageable.OnHit(damage, knockback);
            }
            else
            {
                Debug.LogWarning("Error k");
            }
        }
    }

}
