using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [HideInInspector]
    public static InputHandler Instance;
    [HideInInspector]
    public bool IsMoving { get; set; }
    [HideInInspector]
    public Player CurrentPlayer { get; set; }
    [HideInInspector]
    public Vector3 CurrentPosition { get; set; }

    ActionHistory actionHistory;
    ChangePlayer changePlayer;
    RaycastHit hit;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        changePlayer = FindObjectOfType<ChangePlayer>();
        actionHistory = FindObjectOfType<ActionHistory>();
    }

    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //maybe its not moving, maybe its ressuracting, but need to stop input
            IsMoving = true;
            actionHistory.UndoAction();
        }

        if (Input.GetKeyDown(KeyCode.E) && changePlayer.playersCount > 1)
        {
            IsMoving = true;
            Command command = new ChangePlayerCommand(changePlayer);
            actionHistory.SaveAction(command);
        }
    }
    private void MoveToPosition(Vector3 endPosition, Quaternion rotation)
    {
        IsMoving = true;
        //call command
        Command command = new MovePlayerCommand(CurrentPlayer, CurrentPosition, endPosition, rotation);
        actionHistory.SaveAction(command);

    }
}
