using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    Material[] material;
    float value = 0;
    public float increase = 0.1f;
    public bool execute;
    float length;
    Renderer[] childGO;
    public GameObject nexteffect;
    public bool timeline2played=false;
    private void Awake()
    {

        childGO = GetComponentsInChildren<Renderer>();
        length = GetComponentsInChildren<Transform>().Length;
        //for (int i=0; i< length -1; i++)
        //{
        //    material[i] = childGO[i].material;
        //}
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {execute=true;}

        if (execute)
        {
            if (value <= 1f)
            {
                value += Time.deltaTime * increase;
                for (int i = 0; i < length - 1; i++)
                {
                    //material[i].SetFloat("_Alpha", value);
                    childGO[i].material.SetFloat("_Alpha", value);
                }
            }

             if (timeline2played==false){

            nexteffect.GetComponent<TimelinePlayer2>().StartTimeline();
            timeline2played=true;}



        }
    }
}
