using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Avoid_Player : MonoBehaviour {

    public float speed = 1f;
    public float clampPositionLeft, clampPositionRight;

	void Update () {
        Vector3 motion = Vector3.zero;

        motion.x = Input.GetAxisRaw("Horizontal");

        transform.Translate(motion * speed * Time.deltaTime);

        /*
        // CLAMP POSITION
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, clampPositionLeft, clampPositionRight);

        transform.position = pos;*/
	}
}
