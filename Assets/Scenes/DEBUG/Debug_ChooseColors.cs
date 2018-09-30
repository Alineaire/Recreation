using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_ChooseColors : MonoBehaviour {

    public Color[] colors;
    public ArduinoCommunication arduinoCommunication;

    void Update () {
		for(int i=0; i<colors.Length; i++)
        {
            arduinoCommunication.SetButtonColor(i, colors[i]);
        }
	}
}
