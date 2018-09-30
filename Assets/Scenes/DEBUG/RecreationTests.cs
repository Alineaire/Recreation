using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecreationTests : MonoBehaviour
{

    public ArduinoCommunication arduinoCommunication;
    public int nbrButtons = 15;

    public Color defaultColor = Color.green;
    public Color inputColor = Color.blue;

    void Update()
    {
        for (int i = 0; i < nbrButtons; ++i)
        {
            if (arduinoCommunication.IsButtonPressed(i))
            {
                arduinoCommunication.SetButtonColor(i, inputColor);
                Debug.Log("Input bouton " + i);
            }
            else
            {
                arduinoCommunication.SetButtonColor(i, defaultColor);
            }
        }
    }
}