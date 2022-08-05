using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WEAPONTYPE { 총, 방망이 };
    public WEAPONTYPE type;

    
    public Transform weaponPosition; //무기가 놓일 위치 , 할당해줘야함
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
                //타입에 맞게 로테이션 조절
            {
                case WEAPONTYPE.총:
                    //this.transform.rotation = Quaternion.Euler();
                    break;
                case WEAPONTYPE.방망이:
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
