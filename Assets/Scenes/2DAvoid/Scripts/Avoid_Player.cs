using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Avoid_Player : MonoBehaviour {

    public float speed = 1f;
    public float clampPositionLeft, clampPositionRight;
    public GameObject diePrefab;
    public Renderer meshRenderer;
    public ParticleSystem particles;

    private int playerID;
    private Avoid_GameManager gameManager;
    public int left, right;

	void Update () {
        // DEBUG MOTION
        Vector3 motion = Vector3.zero;

        motion.x = Input.GetAxisRaw("Horizontal");


        // TODO : input Arduino Avoid_GameManager
        // ARDUINO INPUTS
        if (gameManager.arduinoInputs)
        {
            motion.x = 0f;

            if (gameManager.inputs[left])
                motion.x -= 1f;

            if (gameManager.inputs[right])
                motion.x += 1f;
        }

        // CLAMP POSITION & MOTION
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, clampPositionLeft, clampPositionRight);

        transform.position = pos;

        transform.Translate(motion * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Die();

        gameManager.TurnOffPlayerLight(left);
        gameManager.TurnOffPlayerLight(right);
    }

    public void Die()
    {
        gameManager.PlayerDead();
        Instantiate(diePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetPlayerColors(Color _background, Color _edges, Color _trailColor)
    {
        meshRenderer.material.SetColor("_InnerColor", _background);
        meshRenderer.material.SetColor("_OutlineColor", _edges);
        ParticleSystem.MainModule main = particles.main;
        main.startColor = _trailColor;
    }

    public void SetPlayerID(int _id, Avoid_GameManager _gm)
    {
        playerID = _id;
        gameManager = _gm;
    }

    public void SetPlayerInputs(int _left, int _right)
    {
        left = _left;
        right = _right;
    }
}
