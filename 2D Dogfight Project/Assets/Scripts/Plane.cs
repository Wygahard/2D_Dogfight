﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [SerializeField] private TurnManagerSO _turnManager;

    public List<Vector2> Movements = new List<Vector2>();
    public List<Quaternion> Rotations = new List<Quaternion>();

    private void OnEnable()
    {
        _turnManager.OnEndTurn += TurnEnd;
    }


    private void OnDisable()
    {
        _turnManager.OnEndTurn -= TurnEnd;
    }

    private void Start()
    {        
        transform.position = new Vector2(0,-5);
        transform.rotation = Quaternion.identity;
    }
    
    
    private void TurnEnd()
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
            transform.Translate(move);
            transform.rotation = transform.rotation * Rotations[Movements.IndexOf(move)];
        }
        

    }
    
}
