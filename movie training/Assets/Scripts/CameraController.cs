using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotateSpeedX = 2.3f; //좌우 스피드
    [SerializeField] float rotateSpeedY = 3f; //위아래 스피드
    [SerializeField] float limitMinX = -80f; 
    [SerializeField] float limitMaxX = 50f;
    float eulerAngleX;
    float eulerAngleY;

    [SerializeField] float randomCameraShakeValue = 0.23f; //사용되지않음
    [SerializeField] Transform player;
    Vector3 cameraPos;
    Vector3 cameraPosition;
    private void Awake()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public void ToRatate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotateSpeedY;
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

    //public void Shake()
    //{
    //    cameraPos = transform.localPosition;
    //    StartCoroutine(ShakeCamera());
    //}
    //float cameraY;
    //bool isShaking;
    //IEnumerator ShakeCamera()
    //{
    //    //float cameraX = Random.value * randomCameraShakeValue * 2 - randomCameraShakeValue;
    //    cameraY = Random.value * randomCameraShakeValue;
    //    cameraPosition = transform.localPosition;
    //    //cameraPosition.x += cameraX;
    //    //cameraPosition.y += cameraY;
    //    isShaking = true;

    //    yield return new WaitForSeconds(0.3f);
    //    transform.localPosition = cameraPos;
    //    isShaking = false;
    //}

    //private void Update()
    //{
    //    if (isShaking)
    //    {
    //        cameraPosition = Vector3.Lerp(cameraPosition, new Vector3(0, cameraPosition.y + cameraY, 0), 0.7f);
    //        transform.position = cameraPosition;
    //    }
    //}
}
