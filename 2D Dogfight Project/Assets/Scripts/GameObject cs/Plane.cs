using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> Movements = new List<Vector2>();
    public List<Quaternion> Rotations = new List<Quaternion>();

    private void Start()
    {        
        transform.position = new Vector2(0,-5);
        transform.rotation = Quaternion.identity;
    }    
    
    public void TurnEnd()
    {
        MovePlane();        
    }

    private void MovePlane()
    {
        StartCoroutine(MovePlaneRoutine());
    }

    IEnumerator MovePlaneRoutine()
    {
        yield return new WaitForSecondsRealtime(.1f);

        foreach(Vector2 move in Movements)
        {
            yield return new WaitForSecondsRealtime(.8f);
            ICommand command = new MovePlaneCommand(transform, move, Rotations[Movements.IndexOf(move)]);
            CommandInvoker.AddCommand(command);
        }

        yield return new WaitForSecondsRealtime(.8f);
        GameManager.Instance.BeginTurn();
    }    
}
