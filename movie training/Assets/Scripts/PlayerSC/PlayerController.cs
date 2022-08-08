using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    MoveMent moveMent;
    [SerializeField] CameraController cameraController;
    PlayerAnimation playerAnimation;

    private void Awake()
    {
        moveMent = GetComponent<MoveMent>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        moveMent.ToMove(new Vector3(dirX, 0, dirY).normalized);
        playerAnimation.RunAnimation(new Vector3(dirX, 0, dirY).normalized);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveMent.ToJump();
        }

        float cameraX = Input.GetAxis("Mouse X");
        float cameraY = Input.GetAxis("Mouse Y");

        cameraController.ToRatate(cameraX, cameraY);
    }



  
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("GUN") || other.CompareTag("BAT"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<Weapon>().SetWeaponPosition();
                other.GetComponent<Weapon>().isAcquired = true;
                other.GetComponent<Weapon>().canAttack = true;
            }
        }
    }
}
