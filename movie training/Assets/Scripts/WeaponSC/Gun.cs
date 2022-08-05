using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    RaycastHit raycastHit;
    float shootDistance = 777f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Transform gunHole;
    [SerializeField] Transform gunTransform;

    Vector3 dir;
    
    public void Shot()
    {
        dir = gunHole.position - transform.position; //��� ����
        if (Physics.Raycast(transform.position, dir, out raycastHit, shootDistance))
        {   
            if (raycastHit.collider.CompareTag("BUG"))
            {
                //���� ���̴� On -> ����� ������Բ� �ϰ� �״¼Ҹ� ��;;
                raycastHit.transform.GetComponent<DissolveController>().execute = true;
                GameObject effect = Instantiate(hitEffect, raycastHit.transform.position, Quaternion.identity);
                Destroy(effect, 1f);
            }
        }
    }
    void Update()
    {
        Debug.DrawRay(transform.position, dir * 30f, Color.red);
        if (Input.GetMouseButtonDown(1))
        {
            playerAnimation.ShootingAnimation(true); //����
            canAttack = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            playerAnimation.ShootingAnimation(false); //���� ���
            canAttack = false;
        }

        if (canAttack)
        {

            if (Input.GetMouseButton(0)) //�߻�
            {
                canAttack = false;
                Shot();
                //cameraController.Shake();
                //�ѼҸ��� ���� ������?

                StartCoroutine(WeaponCoolTime());
            }
        }
    }

}
