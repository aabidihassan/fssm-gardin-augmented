using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ShowMedia : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public VideoClip videoClip;
    GameObject canvasPlayer;
    private void Awake()
    {
        canvasPlayer = transform.GetChild(4).gameObject;
        canvasPlayer.SetActive(false);
    }
    public void launch() {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
         GameObject defaultCanvas = GameObject.Find("defaultCanvas");
        defaultCanvas.SetActive(false);
        GameObject quad = transform.GetChild(0).gameObject;
        quad.SetActive(false);

     
        canvasPlayer.SetActive(true);      

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
