using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_RandomColor : MonoBehaviour {

    public Color[] colors;
    public float delay = 1f;
    public int nbrButtons = 15;
    float cpt;
    public ArduinoCommunication arduinoCommunication;

    void Update () {
        cpt -= Time.deltaTime;

        if (cpt > 0f)
            return;

        cpt = delay;

        for(int i=0; i<=nbrButtons; i++)
        {
            arduinoCommunication.SetButtonColor(i, colors[Random.Range(0,colors.Length)]);
        }
	}
}
