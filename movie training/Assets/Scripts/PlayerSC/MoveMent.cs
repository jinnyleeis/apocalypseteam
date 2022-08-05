using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private Transform cameraTransform;
    private Vector3 moveDirection;
    private float gravity = -9.81f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        
        characterController.Move(moveSpeed * moveDirection * Time.deltaTime);
    }

    public void ToMove(Vector3 dir)
    {
        Vector3 movedis = cameraTransform.rotation * dir;
        moveDirection = new Vector3(movedis.x, moveDirection.y , movedis.z);
    }

    public void ToJump()
    {
        if(characterController.isGrounded == true)
        {
            moveDirection.y = jumpForce;
        }
        
    }
}
