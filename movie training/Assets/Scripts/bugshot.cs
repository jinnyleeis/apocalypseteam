using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugshot : MonoBehaviour
{
    // Start is called before the first frame update
   private GameObject target;
   public GameObject next;
   bool yes=false;

void Start() {
    {

        next.SetActive(false);
    }
}
void Update()
{
if (Input.GetMouseButtonDown (0)) 
{
target = GetClickedObject() ;

if(target.Equals(gameObject))  //선택된게 나라면
{

if(yes==false){
    print("that's me!");
   next.SetActive(true);
    yes=true;}
    else if(yes==true) {next.SetActive(false);
    yes=false;}
// 
}

}
}

private GameObject GetClickedObject() 
{
        RaycastHit hit;
        GameObject target = null; 
 
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 
         

        if( true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
        {
            //있으면 오브젝트를 저장한다.
            target = hit.collider.gameObject; 
        } 
 
        return target; 
    } 
}


