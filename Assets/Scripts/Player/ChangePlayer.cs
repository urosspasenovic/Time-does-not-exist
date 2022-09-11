using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Vector3 cameraLocalPostion;

    GameObject[] playersArray;
    List<GameObject> players;
    GameObject currentPlayer;
    Movement currentPlayerMovement;
    private void Awake()
    {
        players = new List<GameObject>();
        playersArray = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < playersArray.Length; i++)
        {
            players.Add(playersArray[i]);
            playersArray[i].GetComponent<Movement>().enabled = false;                     
        }
        currentPlayer = players[0];
        mainCamera.transform.parent = currentPlayer.transform;
        currentPlayerMovement = currentPlayer.GetComponent<Movement>();
        currentPlayerMovement.enabled = true;
        mainCamera.transform.localPosition = cameraLocalPostion;
        playersArray = null;
    }

    public void RemovePlayer(GameObject playerToRemove)
    {
        if (players[0] == playerToRemove)
        {
            if(players.Count == 1)
            {
                print("Game over");
                return;
            }
                
            ChangeCurrentPlayer();
            
        }
        else players.Remove(playerToRemove);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && players.Count > 1)
        {
            players.Add(currentPlayer);
            ChangeCurrentPlayer();
            
        }
    }

    private void ChangeCurrentPlayer()
    {
        players.RemoveAt(0);
        currentPlayerMovement.enabled = false;
        currentPlayer = players[0];
        //maybe have queue for Movement script too
        currentPlayerMovement = currentPlayer.GetComponent<Movement>();
        currentPlayerMovement.enabled = true;
        mainCamera.transform.parent = currentPlayer.transform;
        mainCamera.transform.localPosition = cameraLocalPostion;

    }
}
