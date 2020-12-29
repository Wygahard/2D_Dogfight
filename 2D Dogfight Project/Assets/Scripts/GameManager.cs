using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public override void Init()
    {
        base.Init();
    }


    public Action onEndTurn;

   public void EndTurn()
    {
        Debug.Log(onEndTurn);
        onEndTurn?.Invoke();
    }

}
