using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] float pushPower;

    bool isSpaceBar;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) isSpaceBar = true;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigid = hit.collider.attachedRigidbody;

        if (rigid != null && isSpaceBar)
        {
            Vector3 dir = rigid.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            rigid.AddForceAtPosition(dir * pushPower, transform.position, ForceMode.Impulse);
        }
    }
}
