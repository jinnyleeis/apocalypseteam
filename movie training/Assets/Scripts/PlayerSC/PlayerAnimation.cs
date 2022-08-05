using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RunAnimation(Vector3 moveing)
    {
        if (moveing != Vector3.zero)
        {
            animator.SetFloat("moveSpeed", moveing.magnitude);
        }
        else
        {
            animator.SetFloat("moveSpeed", 0f);
        }
        
    }

    public void ShootingAnimation(bool isAiming)
    {
        if (isAiming)
        {
            animator.SetBool("isAiming", true);
        }
        else
        {
            animator.SetBool("isAiming", false);
        }
    }

    public void SwingBatAnimation()
    {
        animator.SetTrigger("isSwing");

    }
}
