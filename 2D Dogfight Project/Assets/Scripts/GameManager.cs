using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{

    public int turncount { get; private set; }

    public event UnityAction OnBeginTurn;
    public event UnityAction OnEndTurn;    

    private void Start()
    {
        turncount = 0;
        BeginTurn();
    }


    //Actions to do at Beginning of a turn
    public void BeginTurn()
    {
        if (OnBeginTurn != null)
        {
            turncount++;
            OnBeginTurn.Invoke();
        }
        else
        {
            Debug.LogWarning("Begin Turn was requested but is empty");
        }
    }

    //Actions to do at End of a Turn
    public void EndTurn()
    {
        //Debug.Log("DropHappened was called");
        if (OnEndTurn != null)
        {
            OnEndTurn.Invoke();
        }
        else
        {
            Debug.LogWarning("End Turn was requested but is empty");
        }
    }


}
