using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJoystick : MonoBehaviour {

    public float joystickInput;

	// Use this for initialization
	void Update () {
        joystickInput = Input.GetAxis("UCONTROL");

    }

}
