using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float swordDamage = 1f;

    public Collider2D swordCollider;

    public float knocbackF = 200f;

    public GameObject player; 

    public Vector3 faceRight = new Vector3 (-0.1813f, 0.2901f, 0);

    public Vector3 faceLeft = new Vector3 (-0.3586f, 0.2901f, 0);

    private void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword set none");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damagableObject = collider.GetComponent<IDamageable>();

        if (damagableObject != null)
        {
            Vector2 dir = (collider.gameObject.transform.position - player.transform.position).normalized;
            Vector2 knockback = dir * knocbackF;

            damagableObject.OnHit(swordDamage, knockback);
        }
        else
        {
            Debug.LogWarning("Error knockback");
        }
    }

    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }

}
