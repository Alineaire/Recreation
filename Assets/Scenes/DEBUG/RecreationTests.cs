using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecreationTests : MonoBehaviour
{

    public ArduinoCommunication arduinoCommunication;
    public int nbrButtons = 15;

    public Color defaultColor = Color.green;
    public Color inputColor = Color.blue;

    public List<BouttonDuCul> boutons;

    private void Start()
    {
        boutons = new List<BouttonDuCul>();
        for(int i=0; i <nbrButtons; i++)
        {
            BouttonDuCul b = new BouttonDuCul();
            b.i = i;
            arduinoCommunication.SetButtonColor(i, defaultColor);
            boutons.Add(b);
        }
    }

    void Update()
    {
        for (int i = 0; i < nbrButtons; ++i)
        {
            if (arduinoCommunication.IsButtonPressed(i))
            {
                if(!boutons[i].active)
                {
                    arduinoCommunication.SetButtonColor(i, inputColor);
                    boutons[i].active = true;
                }
                
                //Debug.Log("Input bouton " + i);
            }
            else
            {
                if (boutons[i].active)
                {
                    arduinoCommunication.SetButtonColor(i, defaultColor);
                    boutons[i].active = false;
                }
            }
        }
    }
}

[System.Serializable]
public class BouttonDuCul
{
    public int i;
    public bool active = false;
}