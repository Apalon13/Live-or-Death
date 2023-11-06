using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public PlayerControler playerStats;

    public float damage;

    public Collider2D swordCollider;

    public string tagEnemy = "Player";

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

    void Update()
    {
        damage = playerStats.damage;   
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
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
