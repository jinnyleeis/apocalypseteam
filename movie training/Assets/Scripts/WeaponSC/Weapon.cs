using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WEAPONTYPE { ��, ����� };
    public WEAPONTYPE type;

    
    public Transform weaponPosition; //���Ⱑ ���� ��ġ , �Ҵ��������
    public PlayerAnimation playerAnimation;
    public Transform player;

    public bool canAttack;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAnimation = player.GetComponent<PlayerAnimation>();
    }

    public void SetWeaponPosition()
    {
        if(weaponPosition != null)
        {
            weaponPosition.DetachChildren();
        }
        else
        {
            this.transform.SetParent(weaponPosition);
            switch (type)
                //Ÿ�Կ� �°� �����̼� ����
            {
                case WEAPONTYPE.��:
                    //this.transform.rotation = Quaternion.Euler();
                    break;
                case WEAPONTYPE.�����:
                    break;
                default:
                    break;
            }
        }
    }

    public void SetOffWeapon()
    {
        weaponPosition.DetachChildren();
    }

    public IEnumerator WeaponCoolTime()
    {
        yield return new WaitForSeconds(0.3f);
        canAttack = true;
    }
}
