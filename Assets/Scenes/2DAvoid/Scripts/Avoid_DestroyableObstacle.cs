using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid_DestroyableObstacle : MonoBehaviour {

    public GameObject objetToDestroy;

	public void DoDestroy()
    {
        if (objetToDestroy != null)
            Destroy(objetToDestroy);
        else
            Destroy(gameObject);
    }
}
