using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Weapon
{
    //������ �� �� ������ ����̰� ������ ƨ���� �������ϳ� �ƴϸ� �������ϳ�..?
    void Update()
    {
        if (canAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canAttack = false;
                playerAnimation.SwingBatAnimation();

                StartCoroutine(WeaponCoolTime());
            }
        }
    }
}
