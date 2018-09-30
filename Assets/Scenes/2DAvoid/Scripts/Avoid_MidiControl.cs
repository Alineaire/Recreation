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
        // GAME KNOBS
        gameManager.speed = Mathf.Lerp(
            speed.min,
            speed.max,
            MidiJack.MidiMaster.GetKnob(01));

        gameManager.size = Mathf.Lerp(
            size.min,
            size.max,
            MidiJack.MidiMaster.GetKnob(02));

        gameManager.delayToRespawn = Mathf.Lerp(
            delayToRespawn.min,
            delayToRespawn.max,
            MidiJack.MidiMaster.GetKnob(03));

        // VISUAL KNOBS
        gameManager.rotationX = Mathf.Lerp(
            rotationX.min,
            rotationX.max,
            MidiJack.MidiMaster.GetKnob(10));

        gameManager.rotationY = Mathf.Lerp(
            rotationY.min,
            rotationY.max,
            MidiJack.MidiMaster.GetKnob(18));

        gameManager.rotationZ = Mathf.Lerp(
            rotationZ.min,
            rotationZ.max,
            MidiJack.MidiMaster.GetKnob(26));

        gameManager.lineNumber = Mathf.Lerp(
            lineNumber.min,
            lineNumber.max,
            MidiJack.MidiMaster.GetKnob(11));

        gameManager.lineHeight = Mathf.Lerp(
            lineHeight.min,
            lineHeight.max,
            MidiJack.MidiMaster.GetKnob(19));

        gameManager.lineHueOffset = Mathf.Lerp(
            lineHueOffset.min,
            lineHueOffset.max,
            MidiJack.MidiMaster.GetKnob(27));

        gameManager.motionSpeed = Mathf.Lerp(
            motionSpeed.min,
            motionSpeed.max,
            MidiJack.MidiMaster.GetKnob(12));
    }
}
