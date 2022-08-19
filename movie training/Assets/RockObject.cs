using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObject : MonoBehaviour
{
    [SerializeField] GameObject[] Tornados;
    GameObject deleteTornado;
    int tornadoCount;
    private void Awake()
    {
        tornadoCount = Tornados.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HOLE"))
        {
            deleteTornado = Tornados[tornadoCount - 1];
        }
    }

    private void Update()
    {
        if (deleteTornado != null && tornadoCount >= 0)
        {
            if (deleteTornado.transform.localScale.x >= 0)
            {
                deleteTornado.transform.localScale = new Vector3(deleteTornado.transform.localScale.x - Time.deltaTime, deleteTornado.transform.localScale.y, deleteTornado.transform.localScale.z - Time.deltaTime);
            }
            else
            {
                tornadoCount--;
                Destroy(deleteTornado);
            }
        }
    }
}
