using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed=2;
   

    
    void Update()
    {

        Vector3 dir=Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow)){

            dir+=Vector3.right;
        }//방향, 속도, 시간

         if (Input.GetKey(KeyCode.LeftArrow)){
            dir+=Vector3.left;

            //transform.position+=transform.Translate(new Vector3.left*speed*Time.deltaTime);
        }

         if (Input.GetKey(KeyCode.UpArrow)){
            dir+=Vector3.forward;

            //transform.position+=transform.Translate(new Vector3.forward*speed*Time.deltaTime);
        }

         if (Input.GetKey(KeyCode.DownArrow)){

            dir+=Vector3.back;

            //transform.position+=transform.Translate(new Vector3.back*speed*Time.deltaTime);
        }//vector3의 back? :(0,0,-1)의 값. 걍 단축키인 것임

        transform.Translate(dir.normalized*speed*Time.deltaTime);
    }
//바꾼이유 동시에 눌렸을때 두 내용합해져서 속도가 더 빨라진다. 벡터 합해져서 커짐
}
