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
        dir = gunHole.position - transform.position; //쏘는 방향
        if (Physics.Raycast(transform.position, dir, out raycastHit, shootDistance))
        {   
            if (raycastHit.collider.CompareTag("BUG"))
            {
                //버그 쉐이더 On -> 슈루룩 사라지게끔 하고 죽는소리 꽥;;
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
            playerAnimation.ShootingAnimation(true); //조준
            canAttack = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            playerAnimation.ShootingAnimation(false); //조준 취소
            canAttack = false;
        }

        if (canAttack)
        {

            if (Input.GetMouseButton(0)) //발사
            {
                canAttack = false;
                Shot();
                //cameraController.Shake();
                //총소리도 나면 좋을듯?

                StartCoroutine(WeaponCoolTime());
            }
        }
    }

}
