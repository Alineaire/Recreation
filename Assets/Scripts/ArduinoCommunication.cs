using CommandMessenger;
using CommandMessenger.Transport.Serial;
using System;
using System.IO.Ports;
using System.Linq;
using UnityEngine;

public class ArduinoCommunication : MonoBehaviour
{
    private enum Command
    {
        UnknownCommand,
        InvalidArgument,

        ReadyRequest,
        ReadyResponse,

        ButtonsStateResponse,

        SetButtonColorRequest,
        SetButtonColorResponse,

        TurnOffRequest,
        TurnOffResponse,
    };

    public int speed = 9600;
    public float intensity = 255f;

    private SerialTransport serialTransport;
    private CmdMessenger cmdMessenger;

    private const int ButtonCount = 15;
    private bool[] buttonStates = new bool[ButtonCount];

    void OnUnknownCommand(ReceivedCommand arguments)
    {
        Debug.LogWarning("Command without attached callback received.");
    }

    void OnInvalidArgument(ReceivedCommand arguments)
    {
        var message = arguments.ReadStringArg();
        Debug.LogWarningFormat("Command with invalid argument received: {0}", message);
    }

    void OnButtonsStateResult(ReceivedCommand arguments)
    {
        for (int i = 0; i < ButtonCount; ++i)
        {
            buttonStates[i] = arguments.ReadBoolArg();
        }
    }

    private void NewLineReceived(object sender, CommandEventArgs e)
    {
        Console.WriteLine(@"Received > " + e.Command.CommandString());
    }

    private void NewLineSent(object sender, CommandEventArgs e)
    {
        Console.WriteLine(@"Sent > " + e.Command.CommandString());
    }

    private void OnEnable()
    {
        serialTransport = new SerialTransport
        {
            CurrentSerialSettings = {
                BaudRate = speed,
                DtrEnable = false,

            }
        };

        for (int i = 0; i < ButtonCount; ++i)
        {
            buttonStates[i] = false;
        }

        RefreshConnection();
    }

    private void OnDisable()
    {
        for (int retry = 0; retry < 10; ++retry)
        {
            var command = new SendCommand((int)Command.TurnOffRequest, (int)Command.TurnOffResponse, 5);
            var result = cmdMessenger.SendCommand(command);
            if (result.Ok)
                break;
        }

        Close();
    }

    void Close()
    {
        cmdMessenger.Disconnect();
        cmdMessenger.Dispose();
        cmdMessenger = null;
    }

    void RefreshConnection()
    {
        var portNames = SerialPort.GetPortNames();

        if (cmdMessenger == null)
        {
            if (portNames.Length > 0)
            {
                var portName = portNames[0];
                Debug.LogFormat("Opening port {0}.", portName);

                serialTransport.CurrentSerialSettings.PortName = portName;
                cmdMessenger = new CmdMessenger(serialTransport, BoardType.Bit16);

                cmdMessenger.Attach(OnUnknownCommand);
                cmdMessenger.Attach((int)Command.UnknownCommand, OnUnknownCommand);
                cmdMessenger.Attach((int)Command.InvalidArgument, OnInvalidArgument);
                cmdMessenger.Attach((int)Command.ButtonsStateResponse, OnButtonsStateResult);

                cmdMessenger.NewLineReceived += NewLineReceived;
                cmdMessenger.NewLineSent += NewLineSent;

                cmdMessenger.Connect();
            }
        }
        else
        {
            var count = portNames.Count(name => name == serialTransport.CurrentSerialSettings.PortName);
            if (count == 0)
            {
                Debug.Log("Port unavailable, closing.");
                Close();
            }
        }
    }

    private void Update()
    {
        RefreshConnection();
    }

    public bool IsButtonPressed(int index)
    {
        return buttonStates[index];
    }

    public void SetButtonColor(int index, Color color)
    {
        if (cmdMessenger == null)
            return;

        Int16 r = (Int16)Mathf.RoundToInt(color.r * intensity);
        Int16 g = (Int16)Mathf.RoundToInt(color.g * intensity);
        Int16 b = (Int16)Mathf.RoundToInt(color.b * intensity);

        var command = new SendCommand((int)Command.SetButtonColorRequest, (int)Command.SetButtonColorResponse, 5);
        command.AddArgument((Int16)index);
        command.AddArgument(r);
        command.AddArgument(g);
        command.AddArgument(b);
        for (int retry = 0; retry < 10; ++retry)
        {
            var result = cmdMessenger.SendCommand(command);
            if (result.Ok)
                break;
        }
    }
}
