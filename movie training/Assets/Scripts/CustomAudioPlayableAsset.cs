using UnityEngine; 
using UnityEngine.Audio; 
using UnityEngine.Playables; 
using UnityEngine.Timeline;

[CreateAssetMenu] 
public class CustomAudioPlayableAsset : PlayableAsset, ITimelineClipAsset 
{ 
public class SeekBehaviour : PlayableBehaviour 
{ 
public AudioClipPlayable clipPlayable;

public override void OnBehaviourPlay(Playable playable, FrameData info) 
{ 
clipPlayable.Seek(playable.GetTime(), 0); 
}

public override void OnBehaviourPause(Playable playable, FrameData info) 
{ 
clipPlayable.Pause(); 
} 
} 


public AudioClip clip; 
public bool propagate;

public ClipCaps clipCaps => ClipCaps.All;

public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) 
{ 
var acp = AudioClipPlayable.Create(graph, clip, looping: false); 
var sp = ScriptPlayable<SeekBehaviour>.Create(graph, 1); 

sp.ConnectInput(0, acp, 0); 
sp.SetInputWeight(0, 1);

sp.GetBehaviour().clipPlayable = acp; 

return sp; 

} 
}