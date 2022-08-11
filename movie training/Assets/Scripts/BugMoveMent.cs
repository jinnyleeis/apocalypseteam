using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMoveMent : MonoBehaviour
{
    public enum Type { 땅, 오른쪽벽, 왼쪽벽 };
    public Type type; 
    Vector3 rotateDir;

    [SerializeField] float moveSpeed = 0.03f;
    Rigidbody rigidbody;
    [SerializeField] float t;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        ReSet();
    }

    private void Update()
    {
        
        Rotation();
        Move();
    }
    void Move()
    {
        rigidbody.MovePosition(this.transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }

    void Rotation()
    {
        //Vector3 rotation = Vector3.Lerp(transform.eulerAngles, rotateDir, 0.01f);
        //rigidbody.MoveRotation(Quaternion.Euler(rotateDir));
        rigidbody.MoveRotation(Quaternion.Slerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(rotateDir), t));
    }

    private void ReSet() 
    {
        if (type.ToString() == "땅")
        {
            rotateDir.Set(0f, Random.Range(0, 360f), 0f);
        }
        else if(type.ToString() == "오른쪽벽")
        {
            rotateDir.Set(Random.Range(0, 360f), 180f, -90f);
        }
        else
        {
            rotateDir.Set(Random.Range(0, 360f), 0f, -90f);
        }
        

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        ReSet();
    }

}
