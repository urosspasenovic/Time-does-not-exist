using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerCommand : Command
{ 

    ChangePlayer changePlayer;
    public ChangePlayerCommand(ChangePlayer changePlayer)
    {
        this.changePlayer = changePlayer;
    }

    public override void Execute()
    {
        changePlayer.ChangePlayers();
    }

    public override void Undo()
    {
        changePlayer.UndoChangePlayer();
    }
}
