using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class timelineplayer : MonoBehaviour
{
    private PlayableDirector director;//나중에 쓸 것임
    public GameObject controlPanel;
    public Camera cinemachinecamera;
    public Camera interactioncamera;

    
    void Start() {
       // gameObject.SetActive(true);
        
    }

    private void Awake()
    {
        Cinemachinecamera();
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        Debug.Log("ended!!");
        Interactioncamera();
        //controlPanel.SetActive(true);
        Debug.Log("ended yeap ");
    }

    private void Director_Played(PlayableDirector obj)
    {
        Debug.Log("started ");
        //controlPanel.SetActive(false);
    }

    public void StartTimeline()
    {
        //gameObject.SetActive(true);
        director.Play();//이걸로도 이걸 실행시킬 수 있고, 바로 실행시킬 수도 있다!
   }

     public void Cinemachinecamera() {
        interactioncamera.enabled = false;
        cinemachinecamera.enabled = true;
    }
    
    // Call this function to enable FPS camera,
    // and disable overhead camera.
    public void Interactioncamera() {
         interactioncamera.enabled = true;
         cinemachinecamera.enabled = false;
    }
}