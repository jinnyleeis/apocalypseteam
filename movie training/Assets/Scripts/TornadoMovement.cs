using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.3f;
    [SerializeField] float moveRadius = 1f;
    [SerializeField] float time;
    [SerializeField] float maxDistance;

    [SerializeField] Vector3 originVector;
    [SerializeField] Vector3 newPos;
    [SerializeField] Vector3 dir;

    private void Start()
    {
        originVector = transform.position;
        StartCoroutine(MoveTornado());
    }

    IEnumerator MoveTornado()
    {
        float t = 0;
        newPos = new Vector3(originVector.x + Random.Range(-moveRadius, moveRadius), originVector.y, originVector.z + Random.Range(-moveRadius, moveRadius));
        maxDistance = (newPos - originVector).magnitude;
        time = maxDistance / moveSpeed;
        dir = newPos - originVector;
        while (time > t)
        {
            t += Time.deltaTime;
            //this.transform.Translate(dir * Time.deltaTime, Space.World);
            //Vector3 goVec = Vector3.MoveTowards(transform.position, newPos, maxDistance);
            transform.position = Vector3.Lerp(this.transform.position, newPos, Time.deltaTime * 0.6f);
            if (time >= 1.5f) break;
            yield return null;
        }
        originVector = newPos;
        StartCoroutine(MoveTornado());
    }

}
