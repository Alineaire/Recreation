using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid_Obstacle : MonoBehaviour {

    public Vector3 direction;
    public float speed = 1f;

    Avoid_GameManager gameManager;
    Quaternion defaultRotation;
    Vector3 scale;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = GameObject.FindObjectOfType<Avoid_GameManager>();

        defaultRotation = transform.rotation;
        scale = transform.localScale;
    }

    private void Update()
    {
        speed = gameManager.speed;

        /*Vector3 objectRotation = transform.rotation.eulerAngles;
        objectRotation.x += gameManager.rotationX;
        objectRotation.y += gameManager.rotationY;
        objectRotation.z += gameManager.rotationZ;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(objectRotation),
            Time.deltaTime);*/

        Vector3 _rot = new Vector3(
            gameManager.rotationX,
            gameManager.rotationY,
            gameManager.rotationZ);
        transform.Rotate(_rot);

        transform.localScale = Vector3.Lerp(
            scale,
            scale * 2f,
            gameManager.size);
    }

    void FixedUpdate () {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}
}
