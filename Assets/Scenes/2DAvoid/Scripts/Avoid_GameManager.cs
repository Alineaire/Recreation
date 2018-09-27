using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid_GameManager : MonoBehaviour {

    [Header("Obstacle Behaviour")]
    public float speed = 1f;
    [Range(0f, 1f)]
    public float size = 1f;
    public float rotationX = 0f;
    public float rotationY = 0f;
    public float rotationZ = 0f;

    [Header("Obstacle Shader")]
    public Material obstacleMaterial;
    public float lineNumber = 2;
    public float lineHeight = 0.64f;
    public float motionSpeed = 1f;
    public float lineHueOffset = 0f;

    private void Update()
    {
        obstacleMaterial.SetFloat("_LineNumber", lineNumber);
        obstacleMaterial.SetFloat("_LineHeight", lineHeight);
        obstacleMaterial.SetFloat("_MotionSpeed", motionSpeed);

        Color lineHue = obstacleMaterial.GetColor("_LineColor");
        float h, s, v;
        Color.RGBToHSV(lineHue, out h, out s, out v);
        h += Time.deltaTime * lineHueOffset;
        obstacleMaterial.SetColor(
            "_LineColor",
            Color.HSVToRGB(h, s, v)
            );
    }
}
