using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBackFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float strenght = 16, delay = 0.15f;
    public UnityEvent OnBegin, OnDone;
    public void PlayFeetback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 dir = (transform.position - sender.transform.position).normalized;
        rb.AddForce(dir, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
