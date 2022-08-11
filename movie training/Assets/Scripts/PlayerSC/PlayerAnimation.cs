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

       animator.SetFloat("moveSpeed", moveing.magnitude);

    }

    public void ShootingAnimation(bool isAiming)
    {

        animator.SetBool("isAiming", isAiming);
    }

    public void SwingBatAnimation()
    {
        animator.SetTrigger("isSwing");

    }

    public void PushingAnimation(bool isPushing)
    {
        animator.SetBool("isPushing", isPushing);

    }
}
