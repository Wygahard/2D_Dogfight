using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlaneCommand : ICommand
{
    Vector2 position;
    Quaternion rotation;
    Transform plane;

    public MovePlaneCommand(Vector2 position, Quaternion rotation, Transform plane)
    {
        this.position = position;
        this.rotation = rotation;
        this.plane = plane;
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
   
}
