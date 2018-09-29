using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinMax
{
    public float min, max;
}

public class Avoid_MidiControl : MonoBehaviour {

    public MinMax speed;
    public MinMax size;
    public MinMax rotationX, rotationY, rotationZ;
    public MinMax delayToRespawn;
    public MinMax lineNumber;
    public MinMax lineHeight;
    public MinMax motionSpeed;
    public MinMax lineHueOffset;

    Avoid_GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<Avoid_GameManager>();
    }

    void Update ()
    {
        gameManager.speed = Mathf.Lerp(
            speed.min,
            speed.max,
            MidiJack.MidiMaster.GetKnob(01));

        gameManager.size = Mathf.Lerp(
            size.min,
            size.max,
            MidiJack.MidiMaster.GetKnob(02));

        gameManager.rotationX = Mathf.Lerp(
            rotationX.min,
            rotationX.max,
            MidiJack.MidiMaster.GetKnob(03));

        gameManager.rotationY = Mathf.Lerp(
            rotationY.min,
            rotationY.max,
            MidiJack.MidiMaster.GetKnob(04));

        gameManager.rotationZ = Mathf.Lerp(
            rotationZ.min,
            rotationZ.max,
            MidiJack.MidiMaster.GetKnob(05));

        gameManager.delayToRespawn = Mathf.Lerp(
            delayToRespawn.min,
            delayToRespawn.max,
            MidiJack.MidiMaster.GetKnob(06));

        gameManager.lineNumber = Mathf.Lerp(
            lineNumber.min,
            lineNumber.max,
            MidiJack.MidiMaster.GetKnob(07));

        gameManager.lineHeight = Mathf.Lerp(
            lineHeight.min,
            lineHeight.max,
            MidiJack.MidiMaster.GetKnob(08));

        gameManager.lineHueOffset = Mathf.Lerp(
            lineHueOffset.min,
            lineHueOffset.max,
            MidiJack.MidiMaster.GetKnob(09));

        gameManager.motionSpeed = Mathf.Lerp(
            motionSpeed.min,
            motionSpeed.max,
            MidiJack.MidiMaster.GetKnob(10));
    }
}
