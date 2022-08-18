using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelinePlayer2 : MonoBehaviour
{
    private PlayableDirector director;//나중에 쓸 것임
    public GameObject camera;
    private bool timeline2isplayed=false;
    



    void Start() {
        //gameObject.SetActive(false);
        
    }

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        Debug.Log("time2 not yet ");
        //controlPanel.SetActive(true);
         if(timeline2isplayed)
        {
            Debug.Log("timeline2 played and  ended");
            Debug.Log("NEXT SCENE");
            SceneManager.LoadScene("greenscene");
        }
    }

    private void Director_Played(PlayableDirector obj)
    {
        Debug.Log("time2 started ");
       //controlPanel.SetActive(false);
    }

    public void StartTimeline()
    {
         //gameObject.SetActive(true);
         camera.GetComponent<timelineplayer>().Cinemachinecamera();
          director.Play();
          timeline2isplayed=true;
   }
}