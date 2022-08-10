using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    RaycastHit raycastHit;
    float shootDistance = 777f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Transform gunHole;
    [SerializeField] Transform gunHole2;
    [SerializeField] Transform gunTransform;

    Vector3 dir;
    Vector3 originRotation;
    private void Awake()
    {
        originRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

    }
    public void Shot()
    {
        dir = gunHole.position - gunHole2.position; //쏘는 방향
        if (Physics.Raycast(transform.position, dir, out raycastHit, shootDistance))
        {   
            if (raycastHit.collider.CompareTag("BUG"))
            {
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
