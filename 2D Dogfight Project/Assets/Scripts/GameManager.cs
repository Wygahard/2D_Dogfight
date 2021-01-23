using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    float turncount;

    private void Awake()
    {
        turncount = 1;

    }

}
