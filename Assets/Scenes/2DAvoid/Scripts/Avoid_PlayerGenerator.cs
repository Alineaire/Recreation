using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Avoid_PlayerSetup
{
    public string label;
    public Color edgeColor, backgroundColor;
    public Vector3 position;
    public Avoid_Player behaviour;
    public int leftButtonID, rightButtonID;
}

public class Avoid_PlayerGenerator : MonoBehaviour {

    public Avoid_PlayerSetup[] players;
    public GameObject playerPrefab;
    public Avoid_GameManager gameManager;

    private void Start()
    {
        InitGame();
    }

    void InitGame()
    {
        int i = 0;
        foreach(Avoid_PlayerSetup player in players)
        {
            GameObject g = Instantiate(
                playerPrefab,
                player.position,
                Quaternion.identity);

            player.behaviour = g.GetComponent<Avoid_Player>();
            // id au cas ou
            player.behaviour.SetPlayerID(i*2, gameManager);
            // couleur du gameobject
            player.behaviour.SetPlayerColors(player.backgroundColor, player.edgeColor, player.edgeColor);
            // inputs arduino
            player.behaviour.SetPlayerInputs(i*2, i*2+1);
            // arduino leds colors
            gameManager.TurnOnPlayerLight(i*2, player.edgeColor);
            gameManager.TurnOnPlayerLight(i * 2 + 1, player.edgeColor);

            i++;
        }
    }
}
