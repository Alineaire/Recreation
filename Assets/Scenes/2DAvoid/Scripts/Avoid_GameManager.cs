using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Controls")]
    public bool arduinoInputs = true;
    public ArduinoCommunication arduinoCommunication;
    public bool[] inputs = new bool[15];

    // arduinoCommunication.IsButtonPressed(i)
    // arduinoCommunication.SetButtonColor(i, color);

    private void Update()
    {
        // DESIGN EVOLUTIONS WITH VARIABLES
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

        // ARDUINO INPUTS
        if (!arduinoInputs)
            return;

        for (int i = 0; i < 15; ++i)
        {
            inputs[i] = arduinoCommunication.IsButtonPressed(i);
        }
    }

    public void TurnOnPlayerLight(int _id, Color _c)
    {
        arduinoCommunication.SetButtonColor(_id, _c);
    }

    public void TurnOffPlayerLight(int _id)
    {
        arduinoCommunication.SetButtonColor(_id, Color.black);
    }
}
