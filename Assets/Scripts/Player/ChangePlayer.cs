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
    Queue<GameObject> players;
    GameObject currentPlayer;
    Movement currentPlayerMovement;
    private void Awake()
    {
        players = new Queue<GameObject>();
        playersArray = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < playersArray.Length; i++)
        {
            players.Enqueue(playersArray[i]);
            playersArray[i].GetComponent<Movement>().enabled = false;                     
        }
        currentPlayer = players.Dequeue();
        mainCamera.transform.parent = currentPlayer.transform;
        currentPlayerMovement = currentPlayer.GetComponent<Movement>();
        currentPlayerMovement.enabled = true;
        mainCamera.transform.localPosition = cameraLocalPostion;
        playersArray = null;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            ChangeCurrentPlayer();
            
        }
    }

    private void ChangeCurrentPlayer()
    {
        currentPlayerMovement.enabled = false;
        players.Enqueue(currentPlayer);
        currentPlayer = players.Dequeue();
        //maybe have queue for Movement script too
        currentPlayerMovement = currentPlayer.GetComponent<Movement>();
        currentPlayerMovement.enabled = true;
        mainCamera.transform.parent = currentPlayer.transform;
        mainCamera.transform.localPosition = cameraLocalPostion;
    }
}
