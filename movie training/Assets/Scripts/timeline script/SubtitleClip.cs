using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleClip : PlayableAsset
{

    public string subtitleText;
    //allows us to set the text in the editor
    // Start is called before the first frame update
    

     public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
   {
       var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

       SubtitleBehaviour SubtitleBehaviour = playable.GetBehaviour(); //playable에서 behaviour 가져옴
       SubtitleBehaviour.subtitleText=subtitleText;//클립 인스펙터창서 입력

       return playable;
       //set the subtitle text on the behaviour from the subtitle text on the clip 

}
}
