using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;//나중에 쓸 것임
    public GameObject controlPanel;
    



    void Start() {
        gameObject.SetActive(false);
        
    }

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        Debug.Log("ended ");
        controlPanel.SetActive(true);
        Debug.Log("ended ");
    }

    private void Director_Played(PlayableDirector obj)
    {
        Debug.Log("started ");
        controlPanel.SetActive(false);
    }

    public void StartTimeline()
    {
         gameObject.SetActive(true);
        director.Play();
   }
}