using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovePlaneMethod
{
    //List of position and rotation in the game
    //static List<Transform> planes;
    
    public static void ForwardPlane(Transform plane, Vector2 position, Quaternion rotation)
    {
        Transform newPlane = plane;
        newPlane.Translate(position);
        newPlane.rotation = newPlane.rotation * rotation;
        
        /*
        if(planes == null)
        {
            planes = new List<Transform>();
        }
        planes.Add(newPlane);
        */
    }

    public static void BackwardPlane(Transform plane, Vector2 position, Quaternion rotation)
    {
        plane.rotation = plane.rotation * Quaternion.Inverse(rotation);
        plane.Translate(-position);        
    }
}
