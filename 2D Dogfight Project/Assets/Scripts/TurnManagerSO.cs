using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TurnManager", menuName = "Game /TurnManager")]
public class TurnManagerSO : ScriptableObject
{
    public event UnityAction OnEndTurn;

    public void EndTurn()
    {
        //Debug.Log("DropHappened was called");
        if (OnEndTurn != null)
        {
            OnEndTurn.Invoke();
        }
        else
        {
            Debug.LogWarning("An End Turn was requested but is empty");
        }
    }
}
