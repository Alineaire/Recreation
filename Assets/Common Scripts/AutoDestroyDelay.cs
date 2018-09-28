using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyDelay : MonoBehaviour {

    public float delay = 1f;

	void Update () {
        delay -= Time.deltaTime;

        if (delay > 0f)
            return;

        Destroy(gameObject);
	}
}
