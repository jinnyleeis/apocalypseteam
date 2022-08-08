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
        playerAnimator.SetTrigger("isShot");
    }

    public override void SetOffWeapon()
    {
        base.SetOffWeapon();
    }
    void Update()
    {
        Debug.DrawRay(transform.position, dir * 30f, Color.red);
        if (isAcquired)
        {
            if (Input.GetMouseButtonDown(1))
            {
                playerAnimator.SetBool("isShotIdleState", true);
                transform.GetChild(0).transform.localPosition = new Vector3(0.1439f, -0.0224f, -0.1518f);
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(17.691f, -171.485f, -6.9f);
                canAttack = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                playerAnimator.SetBool("isShotIdleState", false); 
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(66.014f, -193.442f, -51.479f);
                canAttack = false;
            }

            if (canAttack)
            {

                if (Input.GetMouseButton(0)) //발사
                {
                    canAttack = false;
                    Shot();
                    //총소리도 나면 좋을듯?

                    StartCoroutine(WeaponCoolTime());
                }
            }
        }
        
    }
    IEnumerator WeaponCoolTime()
    {
        yield return new WaitForSeconds(0.3f);
        canAttack = true;
    }
}
