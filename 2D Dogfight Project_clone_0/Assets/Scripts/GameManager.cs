using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState curState;
    public int turncount { get; private set; }

    public event UnityAction OnBeginTurn;
    public event UnityAction OnGamePhase;
    public event UnityAction OnEndTurn;    

    private void Start()
    {
        turncount = 0;
        BeginTurn();
    }


    //Actions to do at Beginning of a turn
    //Called by Plane.MovePlaneRoutine()
    public void BeginTurn()
    {
        if (OnBeginTurn != null)
        {
            turncount++;
            OnBeginTurn.Invoke();
        }
        else
        {
            Debug.LogWarning("BeginTurn was requested but is empty");
        }
    }

    //GamePhase is called by Player.BeginTurn in the coroutine
    public void GamePhase()
    {
        if (OnGamePhase != null)
        {
            OnGamePhase.Invoke();
        }
        else
        {
            Debug.LogWarning("GamePhase was requested but is empty");
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
            Debug.LogWarning("EndTurn was requested but is empty");
        }
    }

    public enum GameState
    {
        NewTurn, //0
        GamePhase, //1
        EndTurn //2
    }

}
