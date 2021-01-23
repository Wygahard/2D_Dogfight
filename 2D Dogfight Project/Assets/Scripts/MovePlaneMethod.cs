using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovePlaneMethod
{
    static List<Transform> planes;
    
    public static void ForwardPlane(Vector2 position, Quaternion rotation, Transform plane)
    {
        Transform newPlane = plane;
        newPlane.Translate(position);
        newPlane.rotation = newPlane.rotation * rotation;

        if(planes == null)
        {
            planes = new List<Transform>();
        }
        planes.Add(newPlane);
    }

    public static void BackwardPlane(Vector2 position, Quaternion rotation)
    {
        for(int i = 0; i < planes.Count; i++)
        {
            //How to keep in memory the movement? Add the card name to the plane ?

        }
    }

}
