using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    int numberOfMovesAfterEnemiesCanMove;
    [SerializeField]
    float timeAfterYouCanStopEnemyMovement;

    [HideInInspector]
    public static InputHandler Instance;
    [HideInInspector]
    public bool IsMoving { get; set; }
    [HideInInspector]
    public Player CurrentPlayer { get; set; }
    [HideInInspector]
    public Vector3 CurrentPosition { get; set; }

    bool gameRunning = false;
    UIHandler uIHandler;
    int pickup = 0;
    ActionHistory actionHistory;
    ChangePlayer changePlayer;
    RaycastHit hit;
    int movesMade = 0;
    float timerForStoppingEnemyMovement = 0;
    bool startTimer = true;
    bool canUsePowerUp = false;
    float undoTiemr = 0.7f;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        changePlayer = FindObjectOfType<ChangePlayer>();
        actionHistory = FindObjectOfType<ActionHistory>();
        uIHandler = FindObjectOfType<UIHandler>();
    }

    void Update()
    {
        if (!gameRunning) return;
        undoTiemr -= Time.deltaTime;
        if (startTimer)
        {
            timerForStoppingEnemyMovement += Time.deltaTime;
            if(timerForStoppingEnemyMovement > timeAfterYouCanStopEnemyMovement)
            {
                //show on UI you can use power up
                canUsePowerUp = true;
                timerForStoppingEnemyMovement = 0;
            }
        }
        if (movesMade >= numberOfMovesAfterEnemiesCanMove)
        {
            StopEnemies.EnableEnemyMovement();
            startTimer = true;
        }
        if (IsMoving) return;       
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Physics.Raycast(CurrentPosition, Vector3.left, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Quaternion.Euler(-90f, 90f, 0));
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Physics.Raycast(CurrentPosition, Vector3.forward, out hit, 2f, layerMask))
            {              
                MoveToPosition(hit.transform.position, Quaternion.Euler(-90f, 180f, 0));
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Physics.Raycast(CurrentPosition, Vector3.right, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Quaternion.Euler(-90f, 270f, 0));
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Physics.Raycast(CurrentPosition, Vector3.back, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Quaternion.Euler(-90f, 0, 0));
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && undoTiemr <= 0)
        {
            //maybe its not moving, maybe its ressuracting, but need to stop input
            IsMoving = true;
            actionHistory.UndoAction();
            undoTiemr = 0.9f;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && changePlayer.playersCount > 1)
        {
            IsMoving = true;
            Command command = new ChangePlayerCommand(changePlayer);
            actionHistory.SaveAction(command);
            return;
        }
        if (Input.GetKeyDown(KeyCode.F) && canUsePowerUp)
        {
            StopEnemies.StopEnemiesMovement();
            canUsePowerUp = false;
            movesMade = 0;
            startTimer = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameRunning)
            {
                gameRunning = false;
                StopEnemies.StopEnemiesMovement();             
                uIHandler.Pause();
            }
            else
            {
                StopEnemies.EnableEnemyMovement();
                uIHandler.StartGame();
            }
        }

        
    }

    

    private void MoveToPosition(Vector3 endPosition, Quaternion rotation)
    {
        IsMoving = true;
        movesMade++;
        //call command
        Command command = new MovePlayerCommand(CurrentPlayer, CurrentPosition, endPosition, rotation);
        actionHistory.SaveAction(command);

    }

    public void AddPickup()
    {
        pickup++;
        uIHandler.ChangeTimeCollected();
        if(pickup == 12)
        {
            uIHandler.GameWon();
            print("You won");
        }
    }
    public void StartGame()
    {
        gameRunning = true;
    }
    public void GameOver()
    {
       uIHandler.GameLost();
    }
}
