using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WEAPONTYPE { 총, 방망이 };
    public WEAPONTYPE type;

    
    public Transform weaponPosition; //���Ⱑ ���� ��ġ , �Ҵ��������
    public Animator playerAnimator;
    public Transform player;

    public bool canAttack;
    public bool isAcquired;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAnimator = player.GetComponent<Animator>();
    }

    public void SetWeaponPosition()
    {
        weaponPosition.DetachChildren();
        this.transform.SetParent(weaponPosition);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public virtual void SetOffWeapon()
    {
        weaponPosition.DetachChildren();
        isAcquired = false;
    }


}
