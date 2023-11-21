using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimations : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void AnimatePosition(Vector2 point) 
    { 
        animator.SetFloat("Horizontal", point.x);
        animator.SetFloat("Vertical", point.y);
        if (animator.GetFloat("Horizontal") > 0)
        {
            sp.flipX = false;
        }
        if (animator.GetFloat("Horizontal") < 0)
        {
            sp.flipX = true;
        }
    }
    public void PlayAnimation(Vector2 movementInput)
    {
        animator.SetBool("Run", movementInput.magnitude > 0);
    }
}