using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCommand : Command
{
    Quaternion playerBodyRotation;
    Vector3 position;
    bool isCurrentPlayer;
    public DeathCommand(Player selectedPlayer, Vector3 position, bool isCurrentPlayer, Quaternion playerBodyRotation)
    {
        this.selectedPlayer = selectedPlayer;
        this.position = position;
        this.isCurrentPlayer = isCurrentPlayer; 
        this.playerBodyRotation = playerBodyRotation;
    }

    public override void Execute()
    {
        selectedPlayer.DestroyPlayer();
    }

    public override void Undo()
    {
        selectedPlayer.RessuractePlayer(position, isCurrentPlayer, playerBodyRotation);
    }
}
