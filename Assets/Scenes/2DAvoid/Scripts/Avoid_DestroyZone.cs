using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid_DestroyZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            other.GetComponent<Avoid_DestroyableObstacle>().DoDestroy();
        }
    }
}
