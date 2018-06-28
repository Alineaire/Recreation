using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]
public class WebcamLive : MonoBehaviour
{
    public WebcamPhotographer webcamPhotographer;

    private void OnEnable()
    {
        GetComponent<Image>().material.mainTexture = webcamPhotographer.webcamTexture;
    }
}
