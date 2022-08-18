using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;
//[TrackClipType(typeof(LightControlAsset))]
//[TrackBindingType(typeof(Light))]

[TrackClipType(typeof(SubtitleClip))]//내용물 옵젝에 묶어> 이거하면, add clip할 수 있게된다. 겜옵으로
[TrackBindingType(typeof(TextMeshProUGUI))]//내용물 옵젝에 묶어
public class SubtitleTrack : TrackAsset
{
   // public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
        //return ScriptPlayable<LightControlMixerBehaviour>.Create(graph, inputCount);
    //}

  public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
  {

    return ScriptPlayable<SubtitleTrackMixer>.Create(graph,inputCount);

  }
}
