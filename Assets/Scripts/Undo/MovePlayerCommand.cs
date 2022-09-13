using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerCommand : Command
{
    Vector3 undoPosition;
    Vector3 executePosition;
    Quaternion rotation;
    public MovePlayerCommand(Player selectedPlayer, Vector3 undoPosition, Vector3 executePosition, Quaternion rotation)
    {
        this.selectedPlayer = selectedPlayer;
        this.undoPosition = undoPosition;
        this.executePosition = executePosition;
        this.rotation = rotation;
    }

    public override void Execute()
    {
        selectedPlayer.Move(executePosition, rotation, undoPosition);
    }

    public override void Undo()
    {
            try
            {
                selectedPlayer.UndoMove(undoPosition, rotation);
            }
            catch (ArgumentOutOfRangeException e)
        {
            Debug.Log("greska u movementu");
        }
    }
}
