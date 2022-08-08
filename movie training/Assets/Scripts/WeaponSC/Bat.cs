using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Weapon
{
    //나무를 벨 때 나무에 방망이가 닿으면 튕겨져 나가야하나 아니면 박혀야하나..?

    void Update()
    {
        if (canAttack && isAcquired)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canAttack = false;
                playerAnimator.SetTrigger("isSwing");
                StartCoroutine(WeaponCoolTime());
            }
        }
    }


    IEnumerator WeaponCoolTime()
    {
        yield return new WaitForSeconds(3f);
        canAttack = true;
    }
    public override void SetOffWeapon()
    {
        base.SetOffWeapon();
    }
}
