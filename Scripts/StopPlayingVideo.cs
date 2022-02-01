using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.Video;
[RequireComponent(typeof(ARTrackedImageManager))]
public class StopPlayingVideo : MonoBehaviour
{
    [SerializeField]
    private float coefficientOfVideo = 1f;

    private float time=0f;
    private TextMeshProUGUI tm;
    private GameObject quad;
    private ARTrackedImageManager imageManager;
    private bool detected = false;
    private bool validePose = false;
    private VideoPlayer video;
    private ARRaycastManager raycastManager;
    private Text text;
    [SerializeField]
    private GameTimer scoreObject;
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 30;
        imageManager = GetComponent<ARTrackedImageManager>();

    }
    private void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }
    private void OnEnable()
    {
        
        imageManager.trackedImagesChanged += OntrackImageChanged;
    }
    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OntrackImageChanged;
    }



    private void OntrackImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        
        foreach (ARTrackedImage img in obj.added)
        {            
            UpdateImage(img);
        }
        foreach (ARTrackedImage img in obj.updated)
        {            
            UpdateImage(img);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        //if the image Target is tracked
        if (trackedImage.trackingState != TrackingState.None && trackedImage.trackingState != TrackingState.Limited)
        {



            text = trackedImage.transform.GetComponentInChildren<Text>();
            quad = trackedImage.transform.GetChild(0).gameObject;

            video = trackedImage.GetComponentInChildren<VideoPlayer>();
            if (!scoreObject.IsPause)
            {
                video.Play();
                scoreObject.play();
            }
            
            //Video player canvas show
            trackedImage.transform.GetChild(2).gameObject.SetActive(true);
            detected = true;
            

        }
        else
        {
            
                video.Pause();
            
            
            scoreObject.pause();
            quad = trackedImage.transform.GetChild(0).gameObject;
            //Video player canvas hide
            trackedImage.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {

        
        if (!scoreObject.IsPause && Application.internetReachability != NetworkReachability.NotReachable)
        {
           time += Time.deltaTime;
        }
        if (detected) {text.text = "Score : " + time.ToString("00") ;}
        
        //poseUpdate();
     }

    private void poseUpdate() {
        if (detected) { 
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        validePose = hits.Count > 0;
        if (validePose)
        {
            tm = quad.GetComponentInChildren<TextMeshProUGUI>();
            Pose p = hits[0].pose;
            p.rotation.x = 0;
            tm.text = $" x : {p.rotation.x} y : {p.rotation.y} z : {p.rotation.z}";
             quad.transform.SetPositionAndRotation(quad.transform.position, hits[0].pose.rotation);

        }
        }
    }

    //https://usaupload.com to upload video file
#warning 'https://usaupload.com to upload video file and get the download link and paste it in the unity videoPlayer URL'
}
