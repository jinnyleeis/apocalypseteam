using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    Material material;
    float value;
    public float increase = 0.1f;
    public bool execute;
    private void Awake()
    {

        material = GetComponent<Renderer>().material;
        value = material.GetFloat("_Alpha");
    }

    public void Update()
    {
        if (execute)
        {
            if (value <= 1f)
            {
                value += Time.deltaTime * increase;
                material.SetFloat("_Alpha", value);
            }
        }
    }
}
