using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid_ObstacleGenerator : MonoBehaviour {

    public GameObject[] obstacles;
    public float delayToRespawn;
    public float minXRespawn, maxXRespawn;
    float cpt;

    private void Update()
    {
        cpt -= Time.deltaTime;

        if (cpt > 0f)
            return;

        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(minXRespawn, maxXRespawn);
        pos += transform.position;

        Instantiate(
            obstacles[Random.Range(0, obstacles.Length - 1)],
            pos,
            Quaternion.identity);

        cpt = delayToRespawn;
    }
}
