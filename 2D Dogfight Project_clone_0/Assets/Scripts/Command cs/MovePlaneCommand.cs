using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlaneCommand : ICommand
{
    Transform plane;
    Vector2 position;
    Quaternion rotation;


    public MovePlaneCommand(Transform plane, Vector2 position, Quaternion rotation)
    {
        this.plane = plane;
        this.position = position;
        this.rotation = rotation;        
    }

    public void Execute()
    {
        MovePlaneMethod.ForwardPlane(plane, position, rotation);
    }

    public void Undo()
    {
        MovePlaneMethod.BackwardPlane(plane, position, rotation);
    }
   
}
