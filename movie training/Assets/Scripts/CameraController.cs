using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotateSpeedX = 1.5f; //좌우 스피드
    [SerializeField] float rotateSpeedY = 3f; //위아래 스피드
    [SerializeField] float limitMinX = -80f; 
    [SerializeField] float limitMaxX = 50f;
    float eulerAngleX;
    float eulerAngleY;

    [SerializeField] Transform player;
    private void Awake()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public void ToRatate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotateSpeedX;
        eulerAngleX -= mouseY * rotateSpeedY;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        //transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        player.rotation = Quaternion.Euler(0, eulerAngleY, 0);
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;


        return Mathf.Clamp(angle, min, max);
    }
}
