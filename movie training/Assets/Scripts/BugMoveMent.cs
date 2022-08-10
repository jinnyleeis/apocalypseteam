using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMoveMent : MonoBehaviour
{
    Vector3 rotateDir;

    [SerializeField] float moveSpeed = 0.03f;
    Rigidbody rigidbody;

    public bool canClimbUpWall;
    public bool isWalking;
    bool isClimbUpWall;

    private void Awake()
    {
        canClimbUpWall = true;
        rigidbody = GetComponent<Rigidbody>();
        ReSet();
    }

    private void Update()
    {
        Move();
        Rotation();
    }
    void Move()
    {
        rigidbody.MovePosition(this.transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }

    void Rotation()
    {
        if (!isClimbUpWall)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, rotateDir, 0.01f);
            rigidbody.MoveRotation(Quaternion.Euler(rotation));
        }

    }

    private void ReSet() 
    {
        rotateDir.Set(0f, Random.Range(-360f, 360f), 0f);

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        ReSet();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (canClimbUpWall)
        {

            isClimbUpWall = true;
            rigidbody.useGravity = false;
            if (collision.collider.CompareTag("WALL1"))
            {
                rigidbody.rotation = Quaternion.Euler(-90f, -90f, 0);
            }
            if (collision.collider.CompareTag("WALL2"))
            {
                rigidbody.rotation = Quaternion.Euler(-90f, 90f, 0);
            }
            if (collision.collider.CompareTag("WALL3"))
            {
                rigidbody.rotation = Quaternion.Euler(-90f, 0, 0);
            }
            if (collision.collider.CompareTag("WALL4"))
            {
                rigidbody.rotation = Quaternion.Euler(-90f, 180f, 0);
            }
            //   if (collision.collider.CompareTag("Box106"))
            //{
            //    rigidbody.rotation = Quaternion.Euler(-90f, 180f, 0);
            //}
            canClimbUpWall = false;
            StartCoroutine(UseGravity());
        }
       
    }


    IEnumerator UseGravity()
    {
        yield return new WaitForSeconds(2f);
        rigidbody.useGravity = true;
        yield return new WaitForSeconds(0.5f);
        isClimbUpWall = false;
        rigidbody.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(10f);
        canClimbUpWall = true;
    }
}
