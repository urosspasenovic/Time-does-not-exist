using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    public int playersCount;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Vector3 cameraLocalPostion;

    GameObject[] playersArray;
    List<Player> players;
    Player currentPlayer;

    private void Start()
    {
        players = new List<Player>();
        playersArray = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < playersArray.Length; i++)
        {
            players.Add(playersArray[i].GetComponent<Player>());                   
        }
        InputHandler.Instance.CurrentPlayer = players[0];
        InputHandler.Instance.CurrentPosition = players[0].transform.position;
        mainCamera.transform.parent = InputHandler.Instance.CurrentPlayer.transform;
        mainCamera.transform.localPosition = cameraLocalPostion;
        playersArray = null;
        playersCount = players.Count;
    }

    public void ChangePlayers()
    {
        ChangeCurrentPlayer();
        InputHandler.Instance.IsMoving = false;
    }

    public void UndoChangePlayer()
    {
        //add last used player to first place
        List<Player> copyList = new List<Player> { players[^1] };
        //add rest in order
        copyList.AddRange(players);
        //remove last used bcs he is first now
        copyList.RemoveAt(copyList.Count - 1);
        players = copyList;
        ChangeToSecondPlayer();
        InputHandler.Instance.IsMoving = false;
    }



    public void RemovePlayer(Player playerToRemove)
    {
        if (players[0] == playerToRemove)
        {           
            ChangeCurrentPlayer();           
        }
        players.RemoveAt(players.Count - 1);
        playersCount--;
    }

    public void ChangeCurrentPlayer()
    {
        //move current player to last in the line
        players.Add(InputHandler.Instance.CurrentPlayer);
        players.RemoveAt(0);
        ChangeToSecondPlayer();

    }
    private void ChangeToSecondPlayer()
    {
        
        InputHandler.Instance.CurrentPlayer = players[0];
        InputHandler.Instance.CurrentPosition = players[0].transform.position;
        mainCamera.transform.parent = InputHandler.Instance.CurrentPlayer.transform;
        mainCamera.transform.localPosition = cameraLocalPostion;
    }
    public void AddPlayer(Player player)
    {
        players.Add(player);
        playersCount++;
    }
   
}
