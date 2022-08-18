using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenfade : MonoBehaviour
{
    // Start is called before the first frame update

    public Image image;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        screennoise();
        
    }

    public void screennoise()
    {

        StartCoroutine(screennosieCoroutine());


    }


    IEnumerator screennosieCoroutine()
    {
        float fadecount=0;
        while (fadecount<1.0f);//알파값 1까지 반복.
        yield return new WaitForSeconds(0.01f);
        image.color=new Color(image.color.r,image.color.g,image.color.b,fadecount);



    }
}
