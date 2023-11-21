using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwordAttack : MonoBehaviour
{
    public float swordDamage = 1f;
    public Collider2D swordCollider;

    private void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword not set");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        col.collider.SendMessage("OnHit", swordDamage);
    }


}
