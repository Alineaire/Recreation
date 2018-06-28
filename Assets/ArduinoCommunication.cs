using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using UnityEngine;

public class ArduinoCommunication : MonoBehaviour
{
    public int comPort = 4;
    public int speed = 9600;

    public int[] pinRangeMin;
    public int[] pinRangeMax; // inclusive

    public int bufferCount = 128;
    byte[] buffer;

    SerialPort port;

    Dictionary<int, bool> buttonDowns = new Dictionary<int, bool>();

    void Awake()
    {
        buffer = new byte[bufferCount];
    }

    void Update()
    {
        if (port == null)
        {
            var portNames = SerialPort.GetPortNames();
            if (portNames.Length > 0)
            {
                var portName = portNames[0];
                Debug.LogFormat("Opening port {0}.", portName);

                port = new SerialPort(portName, speed);
                port.ReadBufferSize = 1024;
                port.NewLine = "\r\n";
                port.ReadTimeout = 1;
                port.Open();
            }
        }
        else if (port.IsOpen)
        {
            try
            {
                int count = port.Read(buffer, 0, bufferCount);
                for (int i = 0; i < count; ++i)
                {
                    byte data = buffer[i];
                    int button = (data & 0x1F);
                    bool down = ((data & 0x20) != 0);
                    buttonDowns[button] = down;
                    if (down)
                        Debug.LogFormat("Button {0} down", button);
                }
            }
            catch (TimeoutException)
            {
            }
            catch (IOException)
            {
                Debug.Log("IOException, closing port.");
                port.Close();
                port = null;
            }
        }
    }

    public int GetButtonCount(int color)
    {
        bool down = false;
        int count = 0;
        for (int pin = pinRangeMin[color]; pin <= pinRangeMax[color]; ++pin)
        {
            if (buttonDowns.TryGetValue(pin, out down) && down)
                ++count;
        }
        return count;
    }
}
