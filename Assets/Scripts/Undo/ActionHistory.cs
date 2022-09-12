using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHistory : MonoBehaviour
{
    Stack<Command> commands;
    void Awake()
    {
        commands = new Stack<Command>();
    }
    public void SaveAction(Command command)
    {
        command.Execute();
        commands.Push(command);
    }

    public void UndoAction()
    {
        if (commands.Count > 0)
        {
            Command lastCommand = commands.Pop();
            lastCommand.Undo();
        }
        else
        {
            print("There is nothing to undo!");
            //don't block input system
            InputHandler.Instance.IsMoving = false;
        }
    }
}