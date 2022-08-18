using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class SubtitleTrackMixer : PlayableBehaviour
{
    // Start is called before the first frame update
     public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {

        // object playerData : object that our track is bound to 

        TextMeshProUGUI text = playerData as TextMeshProUGUI;// 이걸텍메쉬프로로.
        string currentText="";
        float currentAlpha =0f;
        //text.text=subtitleText;

        if (!text){return;}

        int inputCount = playable.GetInputCount();
        for (int i=0; i<inputCount; i++)
        {

            float inputWeight = playable.GetInputWeight(i);

            if (inputWeight>0f)
            {

                ScriptPlayable<SubtitleBehaviour> inputPlayable =(ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);
                SubtitleBehaviour input = inputPlayable.GetBehaviour();
                currentText=input.subtitleText;
                currentAlpha=inputWeight;

            }

        }

        text.text=currentText;
        text.color= new Color(1,1,1,currentAlpha);//weight of the clip ? fade the text in and out 
        //easeinout inspector서 조정

        //base.ProcessFrame(playable,info,playerData);
    }
}
