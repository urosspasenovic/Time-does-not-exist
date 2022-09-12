using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public Player selectedPlayer;
    public abstract void Execute();
    public abstract void Undo();
}
