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
            player.behaviour.SetPlayerID(i, gameManager);
            player.behaviour.SetPlayerColors(player.backgroundColor, player.edgeColor, player.edgeColor);

            // TODO : definir les inputs

            gameManager.TurnOnPlayerLight(i, player.edgeColor);

            i++;
        }
    }
}
