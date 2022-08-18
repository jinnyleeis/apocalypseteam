using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
 


[System.Serializable]
public struct RenderFeatureToggle
{
    public ScriptableRendererFeature feature;
    //public bool isEnabled;
   // public bool iscoroutine;
    
}
 
[ExecuteAlways]
public class RenderFeatureToggler : MonoBehaviour
{

    
    [SerializeField]
    private List<RenderFeatureToggle> renderFeatures = new List<RenderFeatureToggle>();
    [SerializeField]
    private UniversalRenderPipelineAsset pipelineAsset;


    public float delayTime1;
     public float delayTime2;
     public bool iscoroutine=true;
     public bool isEnabled=true;


    void Start() {
    delayTime1=5.0f;
     delayTime2=15.0f;
    }



 
    private void Update()
    {
        foreach (RenderFeatureToggle toggleObj in renderFeatures)
        {
            if(iscoroutine){
            StartCoroutine(CountAttackDelay());}

            toggleObj.feature.SetActive(isEnabled);





        
    




    IEnumerator CountAttackDelay()
{
    toggleObj.feature.SetActive(true);
    yield return new WaitForSeconds(delayTime1);
    toggleObj.feature.SetActive(false);
    yield return new WaitForSeconds(delayTime2);

}

            

        }
    }
}
    

    

