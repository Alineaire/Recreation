using UnityEngine;
using System;
using System.IO;

public class WebcamPhotographer : MonoBehaviour
{
    public int width = 1920;
    public int height = 1080;
    public int fps = 5;
    public int quality = 90;
    public string savePath = "webcam";

    public WebCamTexture webcamTexture { get; private set; } = null;

    public void OnEnable()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            string deviceName = devices[0].name;
            webcamTexture = new WebCamTexture(deviceName, width, height, fps);
            webcamTexture.Play();
            Debug.Log(deviceName);
        }
        else
            Debug.LogWarning("No camera");
    }

    private void OnDisable()
    {
        if (webcamTexture)
        {
            webcamTexture.Stop();
            webcamTexture = null;
        }
    }

    public Sprite TakeSnapshot()
    {
        if (webcamTexture && webcamTexture.isPlaying)
        {
            Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);
            texture.SetPixels(webcamTexture.GetPixels());
            texture.Apply();

            string fileName = DateTime.Now.ToString(@"yyyy-MM-dd_HH-mm-ss") + ".jpg";
            Directory.CreateDirectory(savePath);
            File.WriteAllBytes(Path.Combine(savePath, fileName), texture.EncodeToJPG(quality));

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        else
        {
            Debug.LogWarning("No camera");
            return null;
        }
    }
}
